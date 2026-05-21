# Compute Engine instance templates running Container-Optimized OS.
#
# Each template renders a YAML container declaration into instance metadata
# via templatefile(). Secrets in those declarations come from GCP Secret
# Manager via data sources (see secrets.tf).
#
# trimsuffix removes the trailing newline that templatefile() appends so the
# rendered metadata exactly matches what GCP stores (which has no trailing
# newline). Without it, terraform plan would show perpetual drift.

resource "google_compute_instance_template" "webapi" {
  # name_prefix + create_before_destroy (see lifecycle block at the bottom)
  # so future template changes (image bumps, env var updates, metadata
  # changes) roll forward without name collisions. Instance templates are
  # immutable in GCP — any change forces recreation.
  name_prefix  = "espresso-web-api-vm-instance-template-"
  machine_type = "e2-micro"

  tags = ["http-server", "https-server", "lb-health-check"]

  key_revocation_action_type = "NONE"

  # `container-vm` label is auto-applied by GCP when the source image is COS;
  # leaving labels unset here avoids fighting the GCP-side default.

  disk {
    auto_delete  = true
    boot         = true
    device_name  = "espresso-web-api-vm-instance-template"
    disk_size_gb = 10
    disk_type    = "pd-balanced"
    mode         = "READ_WRITE"
    source_image = "projects/cos-cloud/global/images/cos-101-17162-40-34"
  }

  network_interface {
    network = "default"

    access_config {
      network_tier = "STANDARD"
    }
  }

  metadata = {
    google-logging-enabled = "true"
    gce-container-declaration = trimsuffix(templatefile("${path.module}/templates/webapi-container-declaration.yaml.tpl", {
      webapi_image_tag                  = var.webapi_image_tag
      api_key_android                   = data.google_secret_manager_secret_version.this["api_key_android"].secret_data
      api_key_ios                       = data.google_secret_manager_secret_version.this["api_key_ios"].secret_data
      api_key_web                       = data.google_secret_manager_secret_version.this["api_key_web"].secret_data
      api_key_parser                    = data.google_secret_manager_secret_version.this["api_key_parser"].secret_data
      api_key_dev_ios                   = data.google_secret_manager_secret_version.this["api_key_dev_ios"].secret_data
      api_key_dev_android               = data.google_secret_manager_secret_version.this["api_key_dev_android"].secret_data
      espresso_db_connection_string     = data.google_secret_manager_secret_version.this["espresso_db_connection_string"].secret_data
      slack_analytics_webhook           = data.google_secret_manager_secret_version.this["slack_analytics_webhook"].secret_data
      slack_crash_report_webhook        = data.google_secret_manager_secret_version.this["slack_crash_report_webhook"].secret_data
      slack_news_source_request_webhook = data.google_secret_manager_secret_version.this["slack_news_source_request_webhook"].secret_data
    }), "\n")
  }

  service_account {
    email = "${data.google_project.this.number}-compute@developer.gserviceaccount.com"
    scopes = [
      "https://www.googleapis.com/auth/devstorage.read_only",
      "https://www.googleapis.com/auth/logging.write",
      "https://www.googleapis.com/auth/monitoring.write",
      "https://www.googleapis.com/auth/servicecontrol",
      "https://www.googleapis.com/auth/service.management.readonly",
      "https://www.googleapis.com/auth/trace.append",
    ]
  }

  scheduling {
    automatic_restart   = true
    on_host_maintenance = "MIGRATE"
    preemptible         = false
    provisioning_model  = "STANDARD"
  }

  shielded_instance_config {
    enable_integrity_monitoring = true
    enable_secure_boot          = false
    enable_vtpm                 = true
  }

  reservation_affinity {
    type = "ANY_RESERVATION"
  }

  lifecycle {
    create_before_destroy = true
  }
}

resource "google_compute_instance_template" "dashboard" {
  # Using name_prefix (not fixed name) + create_before_destroy in the lifecycle
  # block below so future template changes (image bumps, env var updates,
  # metadata changes) can roll forward without name collisions. Instance
  # templates are immutable in GCP — any change forces recreation.
  name_prefix  = "espresso-dashboard-instance-template-"
  machine_type = "e2-micro"

  tags = ["http-server", "https-server", "lb-health-check"]

  key_revocation_action_type = "NONE"

  # `container-vm` label is auto-applied by GCP when the source image is COS;
  # leaving labels unset here avoids fighting the GCP-side default.

  disk {
    auto_delete  = true
    boot         = true
    device_name  = "espresso-dashboard-instance-template-2"
    disk_size_gb = 10
    disk_type    = "pd-balanced"
    mode         = "READ_WRITE"
    source_image = "projects/cos-cloud/global/images/cos-101-17162-40-34"
  }

  network_interface {
    network = "default"

    access_config {
      network_tier = "PREMIUM"
    }
  }

  metadata = {
    google-logging-enabled = "true"
    gce-container-declaration = trimsuffix(templatefile("${path.module}/templates/dashboard-container-declaration.yaml.tpl", {
      dashboard_image_tag                    = var.dashboard_image_tag
      api_key_parser                         = data.google_secret_manager_secret_version.this["api_key_parser"].secret_data
      espresso_db_connection_string          = data.google_secret_manager_secret_version.this["espresso_db_connection_string"].secret_data
      espresso_identity_db_connection_string = data.google_secret_manager_secret_version.this["espresso_identity_db_connection_string"].secret_data
      sendgrid_api_key                       = data.google_secret_manager_secret_version.this["sendgrid_api_key"].secret_data
      admin_user_password                    = data.google_secret_manager_secret_version.this["admin_user_password"].secret_data
      server_url                             = var.server_url
      slack_analytics_webhook                = data.google_secret_manager_secret_version.this["slack_analytics_webhook"].secret_data
      slack_crash_report_webhook             = data.google_secret_manager_secret_version.this["slack_crash_report_webhook"].secret_data
      slack_news_source_request_webhook      = data.google_secret_manager_secret_version.this["slack_news_source_request_webhook"].secret_data
    }), "\n")
  }

  service_account {
    email = "${data.google_project.this.number}-compute@developer.gserviceaccount.com"
    scopes = [
      "https://www.googleapis.com/auth/devstorage.read_only",
      "https://www.googleapis.com/auth/logging.write",
      "https://www.googleapis.com/auth/monitoring.write",
      "https://www.googleapis.com/auth/servicecontrol",
      "https://www.googleapis.com/auth/service.management.readonly",
      "https://www.googleapis.com/auth/trace.append",
    ]
  }

  scheduling {
    automatic_restart   = true
    on_host_maintenance = "MIGRATE"
    preemptible         = false
    provisioning_model  = "STANDARD"
  }

  shielded_instance_config {
    enable_integrity_monitoring = true
    enable_secure_boot          = false
    enable_vtpm                 = true
  }

  lifecycle {
    create_before_destroy = true
  }

  reservation_affinity {
    type = "ANY_RESERVATION"
  }
}
