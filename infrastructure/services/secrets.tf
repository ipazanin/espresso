# Secret Manager containers + access bindings for the services stack.
#
# This is #9-light: the secret VALUES are stored in Secret Manager (one source
# of truth, gcloud-rotatable), but the VMs still receive them as env vars in
# their container declaration metadata. Terraform reads the latest value via
# data source (see data block below) and renders it into the gce-container-
# declaration. Switching the apps to fetch from Secret Manager directly is
# #9-full, deferred — see services/README.md.
#
# Secret VERSIONS (the values) are NOT managed by Terraform. Bootstrap and
# rotation happen via `gcloud secrets versions add ...`. This keeps secret
# material out of TF state on the version resource (the rendered template
# metadata still contains plaintext values — that exposure is unchanged from
# the previous secrets.auto.tfvars approach until #9-full lands).

locals {
  secret_ids = [
    "api_key_android",
    "api_key_ios",
    "api_key_web",
    "api_key_parser",
    "api_key_dev_ios",
    "api_key_dev_android",
    "espresso_db_connection_string",
    "espresso_identity_db_connection_string",
    "slack_analytics_webhook",
    "slack_crash_report_webhook",
    "slack_news_source_request_webhook",
    "sendgrid_api_key",
    "admin_user_password",
  ]

  developer_principal = "user:ivan.pazanin1996@gmail.com"
  compute_default_sa  = "${data.google_project.this.number}-compute@developer.gserviceaccount.com"
  ci_service_account  = "espresso-registry-account@espresso-8c4ac.iam.gserviceaccount.com"
}

resource "google_project_service" "secretmanager" {
  service            = "secretmanager.googleapis.com"
  disable_on_destroy = false
}

resource "google_secret_manager_secret" "this" {
  for_each = toset(local.secret_ids)

  secret_id = each.key

  replication {
    auto {}
  }

  labels = {
    managed-by = "terraform"
    stack      = "services"
  }

  depends_on = [google_project_service.secretmanager]

  lifecycle {
    prevent_destroy = true
  }
}

# Developer (manual `terraform apply`) needs to read secret values at plan time.
resource "google_secret_manager_secret_iam_member" "developer_accessor" {
  for_each = google_secret_manager_secret.this

  secret_id = each.value.id
  role      = "roles/secretmanager.secretAccessor"
  member    = local.developer_principal
}

# Default Compute Engine SA used by both MIG VMs. Granted now so #9-full
# (apps fetching from Secret Manager directly at startup) requires zero
# additional IAM changes.
resource "google_secret_manager_secret_iam_member" "compute_sa_accessor" {
  for_each = google_secret_manager_secret.this

  secret_id = each.value.id
  role      = "roles/secretmanager.secretAccessor"
  member    = "serviceAccount:${local.compute_default_sa}"
}

# CI service account (espresso-registry-account) — needed for the GitHub
# Actions deploy job to read secrets during `terraform apply`.
resource "google_secret_manager_secret_iam_member" "ci_sa_accessor" {
  for_each = google_secret_manager_secret.this

  secret_id = each.value.id
  role      = "roles/secretmanager.secretAccessor"
  member    = "serviceAccount:${local.ci_service_account}"
}

data "google_secret_manager_secret_version" "this" {
  for_each = google_secret_manager_secret.this
  secret   = each.value.id
  # version omitted → defaults to "latest". Rotations via
  # `gcloud secrets versions add ...` will trigger a TF diff on the
  # rendered metadata (which forces a MIG rolling replacement).
}
