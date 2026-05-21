# BigQuery dataset receiving the Cloud Billing detailed-usage export.
#
# The export config itself is NOT codified — terraform-provider-google has no
# resource for billing exports (open issue since 2019). Only the destination
# dataset is managed here; wire up the export once via Cloud Console:
#   Billing → Billing export → BigQuery export → Detailed usage cost
#     Project: espresso-8c4ac
#     Dataset: billing_export
# Data lands within a few hours; full backfill within ~5 days.

resource "google_bigquery_dataset" "billing_export" {
  dataset_id    = "billing_export"
  friendly_name = "Cloud Billing detailed usage export"
  description   = "Destination for the Cloud Billing BigQuery export (detailed usage cost). Wired up via Cloud Console — see README."
  location      = "EU"

  lifecycle {
    prevent_destroy = true
  }
}
