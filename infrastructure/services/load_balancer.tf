# HTTPS load balancer fronting the webapi MIG.
#
# Topology:
#   Static IP (espresso-premium-static-ip-address, foundation stack)
#     └── HTTPS forwarding rule espresso-premium-static-ip-address-https-frontend
#           └── target HTTPS proxy espresso-target-proxy
#                 └── SSL certificate mcrt-13b82757-...
#                 └── URL map        espresso
#
# The URL map routes:
#   dashboard.espressonews.co/* → espresso-dashboard-backend-service → dashboard MIG (us-east1-b)
#   everything else             → espresso-webapi-backend-service    → webapi MIG (europe-west3-c)
#
# Port 80 is intentionally not bound. The HTTP forwarding rule + target proxy
# previously existed and routed identically to HTTPS (no LB-level redirect),
# but Monitoring data showed port-80 traffic was 100% bots (0% Croatia vs 72%
# Croatia on HTTPS) so the rule was dropped to save the ~$0.025/hr forwarding-rule
# fee. HSTS on HTTPS protects returning real-user clients from any cached
# `http://` references.
#
# Note: both backend services use the LIVENESS health check for LB health probes,
# even though the webapi MIG uses the READINESS check for auto-heal. Faithful
# to current production.

resource "google_compute_backend_service" "webapi" {
  name                  = "espresso-webapi-backend-service"
  protocol              = "HTTP"
  port_name             = "http"
  timeout_sec           = 30
  load_balancing_scheme = "EXTERNAL"
  session_affinity      = "NONE"
  enable_cdn            = false

  health_checks = [google_compute_health_check.liveness.id]

  backend {
    group           = google_compute_instance_group_manager.webapi.instance_group
    balancing_mode  = "UTILIZATION"
    capacity_scaler = 1.0
    max_utilization = 1.0
  }

  connection_draining_timeout_sec = 300

  log_config {
    enable      = false
    sample_rate = 0
  }
}

resource "google_compute_backend_service" "dashboard" {
  name                  = "espresso-dashboard-backend-service"
  protocol              = "HTTP"
  port_name             = "http"
  timeout_sec           = 30
  load_balancing_scheme = "EXTERNAL"
  session_affinity      = "NONE"
  enable_cdn            = false

  health_checks = [google_compute_health_check.liveness.id]

  backend {
    group           = google_compute_instance_group_manager.dashboard.instance_group
    balancing_mode  = "UTILIZATION"
    capacity_scaler = 1.0
    max_utilization = 1.0
  }

  connection_draining_timeout_sec = 300

  log_config {
    enable      = false
    sample_rate = 0
  }
}

resource "google_compute_url_map" "espresso" {
  name            = "espresso"
  default_service = google_compute_backend_service.webapi.id

  host_rule {
    hosts        = ["dashboard.espressonews.co"]
    path_matcher = "dashboard"
  }

  path_matcher {
    name            = "dashboard"
    default_service = google_compute_backend_service.dashboard.id
  }
}

resource "google_compute_managed_ssl_certificate" "espressonews" {
  name = "mcrt-13b82757-b9e1-468c-acdf-642d8e869034"

  managed {
    domains = [
      "espressonews.co",
      "dashboard.espressonews.co",
    ]
  }

  lifecycle {
    prevent_destroy       = true
    create_before_destroy = true
  }
}

resource "google_compute_target_https_proxy" "espresso" {
  name             = "espresso-target-proxy"
  url_map          = google_compute_url_map.espresso.id
  ssl_certificates = [google_compute_managed_ssl_certificate.espressonews.id]
  quic_override    = "NONE"
  tls_early_data   = "DISABLED"
}

resource "google_compute_global_forwarding_rule" "https" {
  name                  = "espresso-premium-static-ip-address-https-frontend"
  ip_address            = data.google_compute_global_address.premium_static_ip.address
  ip_protocol           = "TCP"
  port_range            = "443-443"
  target                = google_compute_target_https_proxy.espresso.id
  load_balancing_scheme = "EXTERNAL"
}
