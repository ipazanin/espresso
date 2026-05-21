# Public configuration for the services stack.

variable "project_id" {
  description = "GCP project ID."
  type        = string
}

variable "region" {
  description = "Primary GCP region for regional resources in the services stack."
  type        = string
}

variable "server_url" {
  description = "Public server URL exposed to the dashboard for self-referential links and emails."
  type        = string
}

variable "webapi_zone" {
  description = "GCP zone hosting the webapi MIG."
  type        = string
}

variable "dashboard_zone" {
  description = "GCP zone hosting the dashboard MIG. Pinned to us-east1-b to qualify for the GCP free tier."
  type        = string
}

variable "webapi_image_tag" {
  description = "Artifact Registry image tag for the webapi container (e.g. \"2.4.5\"). Bumped per release for rollback-capable deploys."
  type        = string

  validation {
    condition     = can(regex("^[0-9]+\\.[0-9]+\\.[0-9]+$", var.webapi_image_tag))
    error_message = "webapi_image_tag must be X.Y.Z (no v prefix, no suffix). The CI workflow tags images with the bare git tag string."
  }
}

variable "dashboard_image_tag" {
  description = "Artifact Registry image tag for the dashboard container (e.g. \"2.4.5\"). Bumped per release for rollback-capable deploys."
  type        = string

  validation {
    condition     = can(regex("^[0-9]+\\.[0-9]+\\.[0-9]+$", var.dashboard_image_tag))
    error_message = "dashboard_image_tag must be X.Y.Z (no v prefix, no suffix). The CI workflow tags images with the bare git tag string."
  }
}

# Sensitive container env vars (API keys, DB connection strings, Slack
# webhooks, SendGrid key, admin password) now live in GCP Secret Manager.
# See secrets.tf for the secret containers, IAM bindings, and data lookups
# that pipe values into the rendered container declaration.
