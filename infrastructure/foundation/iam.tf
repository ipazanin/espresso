# Service accounts and IAM bindings managed by this stack.
#
# NOT managed here (auto-created or out of scope):
#   - App Engine default SA
#   - Compute Engine default SA
#   - firebase-adminsdk-* (Firebase-managed)
#   - espresso-cluster-service-accou (zombie from GKE, scheduled for cleanup)

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
