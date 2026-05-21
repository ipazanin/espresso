# Compute Engine instance templates running Container-Optimized OS.
#
# Each template renders a YAML container declaration into instance metadata
# via templatefile(). Secrets in those declarations come from sensitive
# variables (see secrets.auto.tfvars).
#
# trimsuffix removes the trailing newline that templatefile() appends so the
# rendered metadata exactly matches what GCP stores (which has no trailing
# newline). Without it, terraform plan would show perpetual drift.

resource "google_compute_instance_template" "webapi" {
  # TODO: same name_prefix + create_before_destroy hardening as the dashboard
  # template below. Today the fragility is masked by the MIG's SUBSTITUTE
  # update policy (max_surge_fixed = 1), but the underlying immutable-template
  # rename collision still exists on any future env-var bump or image change.
  name         = "espresso-web-api-vm-instance-template"
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
      api_key_android                   = var.api_key_android
      api_key_ios                       = var.api_key_ios
      api_key_web                       = var.api_key_web
      api_key_parser                    = var.api_key_parser
      api_key_dev_ios                   = var.api_key_dev_ios
      api_key_dev_android               = var.api_key_dev_android
      espresso_db_connection_string     = var.espresso_db_connection_string
      slack_analytics_webhook           = var.slack_analytics_webhook
      slack_crash_report_webhook        = var.slack_crash_report_webhook
      slack_news_source_request_webhook = var.slack_news_source_request_webhook
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
      api_key_parser                         = var.api_key_parser
      espresso_db_connection_string          = var.espresso_db_connection_string
      espresso_identity_db_connection_string = var.espresso_identity_db_connection_string
      sendgrid_api_key                       = var.sendgrid_api_key
      admin_user_password                    = var.admin_user_password
      server_url                             = var.server_url
      slack_analytics_webhook                = var.slack_analytics_webhook
      slack_crash_report_webhook             = var.slack_crash_report_webhook
      slack_news_source_request_webhook      = var.slack_news_source_request_webhook
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
