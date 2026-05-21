# Foundation Stack

Long-lived infrastructure that rarely changes: networking primitives, IAM, the Artifact Registry repository, and Cloud SQL.

## Resources managed here

| File | Resources |
|---|---|
| `artifact_registry.tf` | `espresso` Docker repository in `europe`, with cleanup policies (dry-run staged) |
| `bigquery.tf` | `billing_export` dataset (EU) — destination for the Cloud Billing export |
| `iam.tf` | Service accounts: `espresso-google-analytics`, `espresso-registry-account` |
| `network.tf` | Static IP `espresso-premium-static-ip-address` |
| `cloud_sql.tf` | Postgres 16 instance `espresso-database`, databases `EspressoDb` and `EspressoIdentity` |

## Cross-stack outputs

None today. The `services` stack discovers foundation resources via `data` lookups by name, not via `terraform_remote_state`.

## Cloud Billing BigQuery export

The dataset `billing_export` is provisioned by `bigquery.tf`. The export configuration itself is **not** codified — `terraform-provider-google` has no resource for billing exports. Wire it up once via Cloud Console:

1. Billing → Billing export → BigQuery export → **Detailed usage cost**
2. Project: `espresso-8c4ac`, dataset: `billing_export`
3. Save. Data appears within a few hours; full backfill within ~5 days.

Once data has landed, query SKU-level cost via `bq`:

```sh
bq query --project_id=espresso-8c4ac --use_legacy_sql=false --format=pretty \
  'SELECT service.description AS service,
          sku.description AS sku,
          ROUND(SUM(cost), 4) AS cost,
          currency
   FROM `espresso-8c4ac.billing_export.gcp_billing_export_v1_0108DF_2031E0_7FA596`
   WHERE _PARTITIONDATE >= DATE_SUB(CURRENT_DATE(), INTERVAL 7 DAY)
   GROUP BY service, sku, currency
   ORDER BY cost DESC LIMIT 30'
```

(Billing-account ID hyphens become underscores in the table name.)

## Artifact Registry cleanup policies

Defined in `artifact_registry.tf` and staged in **dry-run** mode (`cleanup_policy_dry_run = true`). GCP evaluates them and logs decisions to Cloud Logging without acting. Inspect 48h of dry-run output to confirm only stale untagged blobs are targeted:

```sh
gcloud logging read \
  'resource.type="artifactregistry.googleapis.com/Repository" AND logName:"cleanup-policy"' \
  --project=espresso-8c4ac --limit=100 --freshness=2d \
  --format='value(timestamp,jsonPayload.message)'
```

When clean, flip `cleanup_policy_dry_run = false` in a follow-up apply.

## Cloud SQL backups

Automated backups + point-in-time recovery are enabled. The first scheduled backup runs at the next `start_time = 04:00 UTC` window. After enabling, take an inaugural on-demand snapshot so DR coverage starts immediately:

```sh
gcloud sql backups create --instance=espresso-database --project=espresso-8c4ac \
  --description="post-tf-backup-enable baseline"
```

## Safety

The following foundation resources carry `lifecycle { prevent_destroy = true }`. Removing the guard from any of them requires an explicit code change and review.

- `google_sql_database_instance.espresso_database` — three independent layers of protection: `lifecycle.prevent_destroy = true`, the resource-level `deletion_protection = true` default (blocks `terraform destroy`), and `settings.deletion_protection_enabled = true` (blocks gcloud/console deletion outside of TF)
- `google_sql_database.espresso_db`, `google_sql_database.espresso_identity` — only `prevent_destroy`. GCP-level `deletion_protection_enabled` is an *instance*-level flag and does not protect individual databases from `DROP DATABASE` or a forced TF destroy
- `google_compute_global_address.premium_static_ip` — destroying releases the IP; LB + DNS break
- `google_artifact_registry_repository.espresso` — destroying wipes all images
- `google_service_account.google_analytics`, `google_service_account.registry` — recreation produces a new `unique_id`, orphaning external grants and triggering GCP's 30-day soft-delete window for the `account_id`

### Deleting the SQL instance (when actually needed)

In order:
1. Drop or flip `lifecycle.prevent_destroy = true` in `cloud_sql.tf`.
2. Set the resource-level `deletion_protection = false`.
3. Set `settings.deletion_protection_enabled = false` and **apply** — the GCP API rejects the destroy otherwise.
4. `terraform destroy` (targeted).

## State

`gs://espresso-8c4ac-tfstate/foundation/default.tfstate`
