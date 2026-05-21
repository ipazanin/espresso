# Cloud SQL instance and databases.
#
# Safety: both `lifecycle.prevent_destroy` and the resource's `deletion_protection`
# attribute (default true) are in effect. Removing either requires an explicit
# code change.
#
# WARNING — known weaknesses preserved from current production:
#   - settings.backup_configuration.enabled = false  (no backups, per user choice)
#   - settings.ip_configuration.authorized_networks includes 0.0.0.0/0
#   - settings.ip_configuration.ssl_mode = ALLOW_UNENCRYPTED_AND_ENCRYPTED
# These are tracked for a separate hardening pass.

resource "google_sql_database_instance" "espresso_database" {
  name             = "espresso-database"
  database_version = "POSTGRES_16"
  region           = var.region

  settings {
    tier              = "db-f1-micro"
    availability_type = "ZONAL"

    deletion_protection_enabled = true

    disk_size = 10
    disk_type = "PD_SSD"

    location_preference {
      zone = "europe-west3-a"
    }

    backup_configuration {
      enabled                        = false
      point_in_time_recovery_enabled = false
      start_time                     = "04:00"
      transaction_log_retention_days = 7

      backup_retention_settings {
        retained_backups = 7
        retention_unit   = "COUNT"
      }
    }

    ip_configuration {
      ipv4_enabled = true
      ssl_mode     = "ALLOW_UNENCRYPTED_AND_ENCRYPTED"

      authorized_networks {
        name  = "All IP addresses"
        value = "0.0.0.0/0"
      }
      authorized_networks {
        name  = "Home"
        value = "213.149.51.250"
      }
    }
  }

  lifecycle {
    prevent_destroy = true

    # Live state has settings.maintenance_window with day=0/hour=0 (GCP's
    # "unset/any-day" representation), but the Terraform schema requires day
    # in 1-7 and rejects a literal day=0. Ignore the block to avoid perpetual
    # drift.
    #
    # To remove this ignore_changes, you MUST first add a real maintenance_window
    # block (e.g. { day = 7, hour = 3 }) to settings — otherwise the next plan
    # will repeatedly try to null out the GCP-side default.
    ignore_changes = [
      settings[0].maintenance_window,
    ]
  }
}

resource "google_sql_database" "espresso_db" {
  name     = "EspressoDb"
  instance = google_sql_database_instance.espresso_database.name
  charset  = "UTF8"

  lifecycle {
    prevent_destroy = true
  }
}

resource "google_sql_database" "espresso_identity" {
  name     = "EspressoIdentity"
  instance = google_sql_database_instance.espresso_database.name
  charset  = "UTF8"

  lifecycle {
    prevent_destroy = true
  }
}
