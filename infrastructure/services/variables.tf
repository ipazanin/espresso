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
}

variable "dashboard_image_tag" {
  description = "Artifact Registry image tag for the dashboard container (e.g. \"2.4.5\"). Bumped per release for rollback-capable deploys."
  type        = string
}

# -----------------------------------------------------------------------------
# Sensitive configuration.
#
# All values declared below are sourced from secrets.auto.tfvars (gitignored).
# A template with placeholder values lives in secrets.auto.tfvars.example.
#
# These are codified verbatim from the live instance template metadata as a
# short-term improvement over plaintext-in-metadata. Migrating to Secret Manager
# is a planned follow-up.
# -----------------------------------------------------------------------------

variable "api_key_android" {
  description = "WebApi APIKEYSCONFIGURATION__ANDROID."
  type        = string
  sensitive   = true
}

variable "api_key_ios" {
  description = "WebApi APIKEYSCONFIGURATION__IOS."
  type        = string
  sensitive   = true
}

variable "api_key_web" {
  description = "WebApi APIKEYSCONFIGURATION__WEB."
  type        = string
  sensitive   = true
}

variable "api_key_parser" {
  description = "Shared APIKEYSCONFIGURATION__PARSER used by both webapi and dashboard."
  type        = string
  sensitive   = true
}

variable "api_key_dev_ios" {
  description = "WebApi APIKEYSCONFIGURATION__DEVIOS."
  type        = string
  sensitive   = true
}

variable "api_key_dev_android" {
  description = "WebApi APIKEYSCONFIGURATION__DEVANDROID."
  type        = string
  sensitive   = true
}

variable "espresso_db_connection_string" {
  description = "Postgres connection string for the EspressoDb database. Shared by webapi and dashboard."
  type        = string
  sensitive   = true
}

variable "espresso_identity_db_connection_string" {
  description = "Postgres connection string for the EspressoIdentity database. Used by dashboard only."
  type        = string
  sensitive   = true
}

variable "slack_analytics_webhook" {
  description = "Slack incoming-webhook URL for analytics events. Shared by webapi and dashboard."
  type        = string
  sensitive   = true
}

variable "slack_crash_report_webhook" {
  description = "Slack incoming-webhook URL for crash reports. Shared by webapi and dashboard."
  type        = string
  sensitive   = true
}

variable "slack_news_source_request_webhook" {
  description = "Slack incoming-webhook URL for news source requests. Shared by webapi and dashboard."
  type        = string
  sensitive   = true
}

variable "sendgrid_api_key" {
  description = "SendGrid API key used by the dashboard for transactional email."
  type        = string
  sensitive   = true
}

variable "admin_user_password" {
  description = "Initial password for the dashboard admin user."
  type        = string
  sensitive   = true
}
