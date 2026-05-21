# Service accounts and IAM bindings managed by this stack.
#
# NOT managed here (auto-created or out of scope):
#   - App Engine default SA
#   - Compute Engine default SA
#   - firebase-adminsdk-* (Firebase-managed)

resource "google_service_account" "google_analytics" {
  account_id   = "espresso-google-analytics"
  display_name = "espresso-google-analytics"
  description  = "Service account for accessing google analytics data from espresso web server"

  lifecycle {
    prevent_destroy = true
  }
}

resource "google_service_account" "registry" {
  account_id   = "espresso-registry-account"
  display_name = "Espresso Registry Account"
  description  = "Registry (Container) Service Account"

  lifecycle {
    prevent_destroy = true
  }
}

# Project-level role bindings.
# Using google_project_iam_member (additive) rather than _binding or _policy so
# we never accidentally strip roles granted to other principals.

resource "google_project_iam_member" "google_analytics_firebase_analytics_admin" {
  project = var.project_id
  role    = "roles/firebase.analyticsAdmin"
  member  = "serviceAccount:${google_service_account.google_analytics.email}"
}

resource "google_project_iam_member" "registry_artifactregistry_admin" {
  project = var.project_id
  role    = "roles/artifactregistry.admin"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

resource "google_project_iam_member" "registry_artifactregistry_writer" {
  project = var.project_id
  role    = "roles/artifactregistry.writer"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# Unusual: serviceAgent role is normally auto-granted only to GCP's own service
# agents. It was manually bound to this SA in production; codifying as-is.
# Candidate for removal in a follow-up audit.
resource "google_project_iam_member" "registry_artifactregistry_service_agent" {
  project = var.project_id
  role    = "roles/artifactregistry.serviceAgent"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# -----------------------------------------------------------------------------
# Roles granted to espresso-registry-account for its dual purpose as the CI
# deploy SA (in addition to its original Artifact Registry duties above).
# These power the .github/workflows/ci-cd-workflow.yml `deploy` job that runs
# `terraform apply` against the services stack on tag push.
# -----------------------------------------------------------------------------

resource "google_project_iam_member" "registry_compute_instance_admin" {
  project = var.project_id
  role    = "roles/compute.instanceAdmin.v1"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# Needed so Terraform's services-stack apply can assign the default Compute
# Engine SA (the one VMs use) as the instance template's service_account.
resource "google_project_iam_member" "registry_service_account_user" {
  project = var.project_id
  role    = "roles/iam.serviceAccountUser"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# Needed for the services stack's `data "google_compute_network" "default"`
# read at plan time.
resource "google_project_iam_member" "registry_compute_network_viewer" {
  project = var.project_id
  role    = "roles/compute.networkViewer"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# Needed so TF can `secrets.get` each google_secret_manager_secret during
# state refresh. The per-secret secretAccessor bindings (services/secrets.tf)
# grant secrets.versions.access for reading values, but the secretAccessor
# role does NOT include secrets.get — only secretmanager.viewer (or admin)
# does. Without this, plan/apply fails with 403 on the resource refresh.
resource "google_project_iam_member" "registry_secretmanager_viewer" {
  project = var.project_id
  role    = "roles/secretmanager.viewer"
  member  = "serviceAccount:${google_service_account.registry.email}"
}

# Read/write/lock the Terraform state bucket. Bucket-scoped (not project-wide)
# so the blast radius is just this bucket — sibling buckets in the project
# are unaffected.
data "google_storage_bucket" "tfstate" {
  name = "espresso-8c4ac-tfstate"
}

resource "google_storage_bucket_iam_member" "registry_tfstate_object_admin" {
  bucket = data.google_storage_bucket.tfstate.name
  role   = "roles/storage.objectAdmin"
  member = "serviceAccount:${google_service_account.registry.email}"
}
