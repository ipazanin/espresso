# Artifact Registry repositories.
#
# Hosts the espresso-webapi and espresso-dashboard Docker images consumed by
# the COS instance templates in the services stack.

resource "google_artifact_registry_repository" "espresso" {
  location      = "europe"
  repository_id = "espresso"
  format        = "DOCKER"

  cleanup_policy_dry_run = true

  docker_config {
    immutable_tags = false
  }

  lifecycle {
    prevent_destroy = true
  }
}
