# Foundation Stack

Long-lived infrastructure that rarely changes: networking primitives, IAM, the Artifact Registry repository, and Cloud SQL.

## Resources managed here

| File | Resources |
|---|---|
| `artifact_registry.tf` | `espresso` Docker repository in `europe` |
| `iam.tf` | Service accounts: `espresso-google-analytics`, `espresso-registry-account` |
| `network.tf` | Static IP `espresso-premium-static-ip-address` |
| `cloud_sql.tf` | Postgres 13 instance `espresso-database`, databases `EspressoDb` and `EspressoIdentity` |

## Cross-stack outputs

None today. The `services` stack discovers foundation resources via `data` lookups by name, not via `terraform_remote_state`.

## Safety

The following foundation resources carry `lifecycle { prevent_destroy = true }`. Removing the guard from any of them requires an explicit code change and review.

- `google_sql_database_instance.espresso_database` (plus its top-level `deletion_protection = true` default)
- `google_sql_database.espresso_db`, `google_sql_database.espresso_identity`
- `google_compute_global_address.premium_static_ip` — destroying releases the IP; LB + DNS break
- `google_artifact_registry_repository.espresso` — destroying wipes all images
- `google_service_account.google_analytics`, `google_service_account.registry` — recreation produces a new `unique_id`, orphaning external grants and triggering GCP's 30-day soft-delete window for the `account_id`

## State

`gs://espresso-8c4ac-tfstate/foundation/default.tfstate`
