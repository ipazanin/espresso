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

Both instance templates pin their container image to a specific tag in Artifact Registry, sourced from TF variables `webapi_image_tag` and `dashboard_image_tag` in `terraform.tfvars`. The CI workflow at `.github/workflows/ci-cd-workflow.yml` tags every release image with both `:${git_tag}` and `:latest` so the pinned reference always exists in AR.

### Deploying a new release

1. Push a new git tag (e.g. `2.4.6`) — CI builds and pushes the dual-tagged images.
2. Update `terraform.tfvars` — bump `webapi_image_tag` and `dashboard_image_tag` to `"2.4.6"`. Keep them in sync; bumping only one is allowed but untested.
3. `terraform apply` — webapi rolls SUBSTITUTE (zero-downtime), dashboard rolls RECREATE (5-10 min outage, see [Rolling out template changes](#rolling-out-template-changes--what-to-expect)).

### Rolling back

Same procedure with a previous tag (e.g. `2.4.5` after `2.4.6` ships). **Bootstrap floor: `2.4.5`** — older git tags (`2.4.4`, `2.4.3`, the `v2.x` series) do NOT have corresponding Docker image tags in AR; only `:latest` was pushed before the dual-tag CI workflow. Rolling back below `2.4.5` would require either rebuilding the image at the old tag or pulling a digest manually.

A future #6 work item automates the tfvars bump on git-tag push so step 2 above goes away.

## Secrets

Sensitive container-declaration env vars (DB connection strings, API keys, Slack webhooks, SendGrid key, admin password) are sourced from `secrets.auto.tfvars` (gitignored). The committed `secrets.auto.tfvars.example` shows the expected shape with `REPLACE_ME` placeholders.

To populate `secrets.auto.tfvars` on a fresh checkout, read live values out of the GCP instance template metadata:

```sh
gcloud compute instance-templates describe espresso-web-api-vm-instance-template
gcloud compute instance-templates list \
  --filter="name~'espresso-dashboard-instance-template-'" \
  --sort-by=~creationTimestamp --limit=1
```

The dashboard template uses `name_prefix`, so its current name has a generated suffix — list and sort by creation time to find the live one before describing it.

A future hardening pass will migrate these to Secret Manager and remove the gitignored file. Tracked as Phase 2 work — requires .NET app changes to use `Google.Cloud.SecretManager.V1` since COS does not natively inject SM values into container env vars.

### Secret exposure surface beyond git

Keeping `secrets.auto.tfvars` out of git is **not** the whole story. As long as secrets are values in Terraform state, they are also accessible via:

- **`terraform show` / `terraform output`** — prints the GCS state file contents in cleartext. IAM on the state bucket `espresso-8c4ac-tfstate` is therefore equivalent to access to these secrets. Treat that bucket's grants the same way you'd treat Secret Manager grants.
- **`plan.out` files** — `terraform plan -out plan.out` embeds the secret values in the binary plan. `plan.out` is gitignored, but do not attach it to issues, share over Slack, or upload as a CI artifact.
- **New outputs** — if you add a `google_*` resource that surfaces a secret, the output block must be declared `sensitive = true` or the value will print to terminals and CI logs.
- **Literal `$` in secret values** — passes through `templatefile()`, so a `$` in a rotated password must be doubled to `$$`. See note in `secrets.auto.tfvars.example`.

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

## Known cosmetic diff

`terraform plan` will permanently report `0 to add, 2 to change, 0 to destroy` against the two instance templates with the warning:

> this attribute value will be marked as sensitive and will not display in UI output after applying this change. **The value is unchanged.**

Source: TF tags the `metadata.gce-container-declaration` value as sensitive because it's interpolated from sensitive variables, but the underlying string is identical to live. Instance templates are immutable in GCP, so applying this carries replacement risk for no real benefit. The diff will clean itself up the next time we make a legitimate change to either template (env var update, image bump, etc.). Until then, ignore the noise — do not blindly `apply` it.
