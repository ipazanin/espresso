# Lookups for resources managed by the foundation stack or by GCP itself.

data "google_project" "this" {
  project_id = var.project_id
}

data "google_compute_network" "default" {
  name = "default"
}

data "google_compute_global_address" "premium_static_ip" {
  name = "espresso-premium-static-ip-address"
}
