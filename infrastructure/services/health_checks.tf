# Global HTTP health checks used by the managed instance groups for auto-healing.
#
# NOT managed here:
#   - espresso-app-health-check (legacy, tied to decommissioned app MIG)

resource "google_compute_health_check" "readiness" {
  name                = "espresso-readiness-health-check"
  check_interval_sec  = 10
  timeout_sec         = 8
  healthy_threshold   = 1
  unhealthy_threshold = 10

  http_health_check {
    port         = 80
    request_path = "/health/readiness"
  }

  log_config {
    enable = false
  }
}

resource "google_compute_health_check" "liveness" {
  name                = "espresso-liveness-health-check"
  check_interval_sec  = 80
  timeout_sec         = 10
  healthy_threshold   = 2
  unhealthy_threshold = 3

  http_health_check {
    port         = 80
    request_path = "/health/liveness"
  }
}
