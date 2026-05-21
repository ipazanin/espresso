# Services Stack

Workload-facing infrastructure that changes with each deploy cycle: instance templates, managed instance groups, and the HTTPS load balancer.

## Resources managed here

| File | Resources |
|---|---|
| `instance_templates.tf` | `espresso-web-api-vm-instance-template`, `espresso-dashboard-instance-template-*` (timestamp-suffixed via `name_prefix`) |
| `managed_instance_groups.tf` | `espresso-web-api-instance-group`, `espresso-dashboard-instance-group` |
| `load_balancer.tf` | Backend services, URL map `espresso`, HTTP + HTTPS target proxies, both forwarding rules, managed SSL certificate |

## Cross-stack inputs

Foundation resources are looked up by name via data sources in `data.tf`:

- Static IP `espresso-premium-static-ip-address` (for forwarding rules)
- Service accounts (referenced by instance templates if needed)
- The `default` VPC (also a data lookup; not managed here)

## What is NOT in this stack

- `espresso-app-instance-group`, `espresso-app-instance-template-v1`, `espresso-app-backend-service` — legacy, scheduled for decommissioning out-of-band.

## State

`gs://espresso-8c4ac-tfstate/services/default.tfstate`

## Image tags and deploys

Both instance templates pin their container image to a specific tag in Artifact Registry. The pinned values live in `deploy.auto.tfvars` (bot-managed), separated from human-edited config in `terraform.tfvars` so release commits don't pollute the blame history of the latter.

### Deploying a new release (fully automated)

1. Push a new git tag in `X.Y.Z` format (e.g. `2.4.9`). That's it.

The CI workflow's `deploy` job (`.github/workflows/ci-cd-workflow.yml`):
1. Waits for both image-build jobs to finish (dual-tagged push of `:X.Y.Z` and `:latest` to AR).
2. Updates `infrastructure/services/deploy.auto.tfvars` to the new tag via `sed`.
3. Runs `terraform init` + `terraform apply -auto-approve` against the services stack.
4. webapi MIG rolls SUBSTITUTE (zero-downtime). Dashboard MIG rolls RECREATE (5–10 min outage, see [Rolling out template changes](#rolling-out-template-changes--what-to-expect)).
5. Commits the updated `deploy.auto.tfvars` back to `master` with a `chore(deploy): pin services to X.Y.Z [skip ci]` message, signed by `github-actions[bot]`. The `[skip ci]` prevents the commit from re-triggering the workflow.
6. Waits for both MIGs to be stable (timeouts: 1500s webapi, 1800s dashboard) before declaring success.

Concurrency `deploy-services` serializes deploys so two tags pushed close together cannot race.

### Rolling back

Use a forward-only version bump: revert the bad commits, tag a fresh `X.Y.(Z+1)`, push. The strict semver regex on the image_tag variables (`variables.tf`) rejects reuse-with-suffix patterns like `2.4.8-rollback`, which is intentional — linear semver history is easier to reason about than ambiguous rollback tags. **Bootstrap floor: `2.4.5`** — older git tags do not have corresponding Docker images in AR; rolling back below `2.4.5` requires rebuilding from old source first.

If CI is broken and you need to deploy manually: edit `deploy.auto.tfvars` locally, `terraform apply` from `infrastructure/services/`. State and code stay aligned.

## Secrets

The 13 sensitive container env vars (API keys, DB connection strings, Slack webhooks, SendGrid key, admin password) live in **GCP Secret Manager** under project `espresso-8c4ac`. The `secrets.tf` file in this stack declares one `google_secret_manager_secret` container per value, plus accessor IAM bindings for the developer and the VMs' default Compute Engine SA. Values themselves are NOT managed by Terraform — they're populated and rotated via `gcloud`.

### Rotating a secret

```sh
printf 'new-value' | gcloud secrets versions add <secret_name> --data-file=- --project=espresso-8c4ac
terraform apply   # in infrastructure/services/ — picks up "latest" and rolls both MIGs
```

The data source defaults to `latest`, so any new version triggers a TF diff on the rendered container declaration → MIG rolling replacement (webapi zero-downtime SUBSTITUTE, dashboard 5-10 min RECREATE outage). Rotating during a low-traffic window is recommended.

To pin a specific version (avoid roll-on-rotation), add `version = "<n>"` to the data source in `secrets.tf` and bump deliberately.

### What this does NOT yet protect against

`#9-light` puts secrets in their proper canonical home but the VMs still **receive** them as plaintext env vars in the `gce-container-declaration` instance template metadata. Anyone with `compute.instances.get` can read them. The same plaintext is also stored in the Terraform state file in GCS (encrypted at rest, accessible to anyone with `storage.objectViewer` on `espresso-8c4ac-tfstate`).

The full security win comes from **`#9-full`** (deferred): switch the .NET apps in `Espresso.WebApi` and `Espresso.Dashboard` to fetch secrets from Secret Manager directly at startup (via `Google.Cloud.SecretManager.V1` + an `IConfigurationProvider`). Once that lands, env vars come out of instance template metadata entirely and the only place secret values exist outside SM is in process memory.

### State and plan output still contain plaintext

- `terraform show` / `terraform output` print state contents in cleartext. IAM on `espresso-8c4ac-tfstate` is therefore secret-equivalent.
- `terraform plan -out plan.out` embeds secret values in the binary plan. `plan.out` is gitignored; do not share over CI artifacts/Slack/etc.
- Any future `output` block referencing a secret must be `sensitive = true` or the value prints to terminals and CI logs.
- Literal `$` in secret values passes through `templatefile()` and must be doubled to `$$` to escape template interpolation. If a rotation introduces a `$`, double it in the value pushed to SM.

### Bootstrapping secrets in a fresh project

If recreating the project from scratch, the data sources in `secrets.tf` will fail at plan time until each secret has at least one version. Two clean paths:

**Option 1 — placeholder bootstrap** (recommended, single `terraform apply`):

1. Before running TF, push a placeholder version for each of the 13 secrets:
   ```sh
   for name in api_key_android api_key_ios api_key_web api_key_parser \
               api_key_dev_ios api_key_dev_android \
               espresso_db_connection_string espresso_identity_db_connection_string \
               slack_analytics_webhook slack_crash_report_webhook \
               slack_news_source_request_webhook sendgrid_api_key admin_user_password; do
     gcloud secrets create "$name" --replication-policy=automatic --project=espresso-8c4ac
     printf 'PLACEHOLDER' | gcloud secrets versions add "$name" --data-file=- --project=espresso-8c4ac
   done
   ```
2. `terraform apply` — adopts the existing secrets (`prevent_destroy` + import not needed since they were created with the same names TF expects). Renders placeholder values into instance templates.
3. Replace each placeholder with the real value: `printf 'REAL_VALUE' | gcloud secrets versions add <name> --data-file=- --project=espresso-8c4ac`.
4. `terraform apply` — picks up `latest`, rolls both MIGs (webapi zero-downtime, dashboard 5–10 min).

**Option 2 — two-phase apply** (cleaner TF, more steps):

1. Comment out the `data "google_secret_manager_secret_version"` block in `secrets.tf` AND the data-source references in `instance_templates.tf`. `terraform apply` to create the 13 empty secret containers + IAM bindings.
2. Push real values via `gcloud secrets versions add` (no placeholder).
3. Uncomment the data blocks + references. `terraform apply` again — renders real values, rolls both MIGs once.

## Rolling out template changes — what to expect

Updating an instance template (image bump, env-var change) triggers a MIG-driven instance replacement. The two MIGs behave differently:

- **webapi** — `update_policy.replacement_method = SUBSTITUTE` with `max_surge_fixed = 1`. A second instance comes up alongside the old one and traffic shifts when health checks pass. Effectively zero-downtime.
- **dashboard** — `update_policy.replacement_method = RECREATE` with `max_surge_fixed = 0` and `max_unavailable_fixed = 1`. The existing instance is destroyed *before* the replacement boots. Combined with `auto_healing_policies.initial_delay_sec = 180` on the liveness check plus COS image pull and .NET cold-start, the dashboard is dark for **5–10 minutes** during any template change (observed). This is faithful to live config — recreating in place is constrained by the single static external IP attached to the dashboard VM. Do dashboard rollouts in low-traffic windows. The instance template itself rolls forward safely via `name_prefix` + `create_before_destroy`; the downtime is purely a MIG update-policy property, not a template-rename property.

## Adding or changing SSL-cert domains

`google_compute_managed_ssl_certificate.espressonews` carries both `prevent_destroy = true` and `create_before_destroy = true`. Adding a domain (e.g. a second subdomain) is non-trivial because managed certificates are immutable on `managed.domains` — the resource must be replaced.

Procedure:

1. Drop `prevent_destroy = true` on the resource (commit, then `terraform plan` to verify the replacement is what you expect).
2. Update `managed.domains`. `create_before_destroy` ensures the new cert is provisioned first.
3. **Critical**: managed cert provisioning blocks on DNS validation; the new cert won't go ACTIVE until DNS records resolve for all listed domains. The old cert keeps serving until then.
4. After the new cert is ACTIVE, `terraform apply` removes the old one. The `target_https_proxy.espresso.ssl_certificates` list is automatically updated by TF as part of the same apply.
5. Restore `prevent_destroy = true`.

DNS for `espressonews.co` lives outside GCP (no Cloud DNS zones in this project), so add records there before applying.

