# Services Stack

Workload-facing infrastructure that changes with each deploy cycle: instance templates, managed instance groups, and the HTTPS load balancer.

## Resources managed here

| File | Resources |
|---|---|
| `instance_templates.tf` | `espresso-web-api-vm-instance-template`, `espresso-dashboard-instance-template` |
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

## Secrets

Sensitive container-declaration env vars (DB connection strings, API keys, Slack webhooks, SendGrid key, admin password) are sourced from `secrets.auto.tfvars` (gitignored). The committed `secrets.auto.tfvars.example` shows the expected shape with `REPLACE_ME` placeholders.

To populate `secrets.auto.tfvars` on a fresh checkout, read live values out of the GCP instance template metadata:

```sh
gcloud compute instance-templates describe espresso-web-api-vm-instance-template
gcloud compute instance-templates describe espresso-dashboard-instance-template
```

A future hardening pass will migrate these to Secret Manager and remove the gitignored file. Tracked as Phase 2 work — requires .NET app changes to use `Google.Cloud.SecretManager.V1` since COS does not natively inject SM values into container env vars.

## Known cosmetic diff

`terraform plan` will permanently report `0 to add, 2 to change, 0 to destroy` against the two instance templates with the warning:

> this attribute value will be marked as sensitive and will not display in UI output after applying this change. **The value is unchanged.**

Source: TF tags the `metadata.gce-container-declaration` value as sensitive because it's interpolated from sensitive variables, but the underlying string is identical to live. Instance templates are immutable in GCP, so applying this carries replacement risk for no real benefit. The diff will clean itself up the next time we make a legitimate change to either template (env var update, image bump, etc.). Until then, ignore the noise — do not blindly `apply` it.
