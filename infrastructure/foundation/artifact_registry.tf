# Artifact Registry repositories.
#
# Hosts the espresso-webapi and espresso-dashboard Docker images consumed by
# the COS instance templates in the services stack.
#
# Cleanup policies run automatically on a GCP-managed schedule (roughly daily).
# Released semver tags and `:latest` are protected forever; untagged blobs
# older than 30 days are reaped. To pause cleanup (e.g. before a risky
# investigation), flip `cleanup_policy_dry_run = true` and apply.

resource "google_artifact_registry_repository" "espresso" {
  location      = "europe"
  repository_id = "espresso"
  format        = "DOCKER"

  cleanup_policy_dry_run = false

  docker_config {
    immutable_tags = false
  }

  # Released semver tags (X.Y.Z) are the rollback inventory; never delete.
  # `tag_prefixes` is a literal prefix match (not regex); the digit list covers
  # every legal semver leading character.
  cleanup_policies {
    id     = "keep-released-semver"
    action = "KEEP"
    condition {
      tag_state    = "TAGGED"
      tag_prefixes = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]
    }
  }

  cleanup_policies {
    id     = "keep-latest-floating-tag"
    action = "KEEP"
    condition {
      tag_state    = "TAGGED"
      tag_prefixes = ["latest"]
    }
  }

  # Redundant safety net so a misplaced tag movement can't strand the live
  # deployment. The per-package most-recent set always protects whatever
  # `deploy.auto.tfvars` currently pins.
  cleanup_policies {
    id     = "keep-recent-versions-per-package"
    action = "KEEP"
    most_recent_versions {
      keep_count = 10
    }
  }

  cleanup_policies {
    id     = "delete-stale-untagged"
    action = "DELETE"
    condition {
      tag_state  = "UNTAGGED"
      older_than = "2592000s" # 30 days
    }
  }

  lifecycle {
    prevent_destroy = true
  }
}
