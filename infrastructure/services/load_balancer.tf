# HTTPS load balancer fronting the webapi MIG.
#
# Topology:
#   Static IP (espresso-premium-static-ip-address, foundation stack)
#     ├── HTTP  forwarding rule espresso-premium-static-ip-address-http-frontend
#     │     └── target HTTP proxy  espresso-target-proxy-2
#     │           └── URL map      espresso
#     │                 └── default backend service espresso-webapi-backend-service
#     │                       └── MIG espresso-web-api-instance-group
#     └── HTTPS forwarding rule espresso-premium-static-ip-address-https-frontend
#           └── target HTTPS proxy espresso-target-proxy
#                 └── SSL certificate mcrt-13b82757-...
#                 └── URL map        espresso (same as HTTP side)
#
# espresso-dashboard-backend-service exists but is orphaned (the URL map's
# default_service is webapi-backend-service and there are no path matchers).
# Codified here for parity with live state. Future decision: either wire it
# into the URL map via host_rule + path_matcher, or retire it. The dashboard
# subdomain currently resolves to the LB IP (DNS at Namecheap) but the LB
# routes that traffic to webapi, not here.
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

resource "google_compute_target_http_proxy" "espresso" {
  name    = "espresso-target-proxy-2"
  url_map = google_compute_url_map.espresso.id
}

resource "google_compute_target_https_proxy" "espresso" {
  name             = "espresso-target-proxy"
  url_map          = google_compute_url_map.espresso.id
  ssl_certificates = [google_compute_managed_ssl_certificate.espressonews.id]
  quic_override    = "NONE"
  tls_early_data   = "DISABLED"
}

resource "google_compute_global_forwarding_rule" "http" {
  name                  = "espresso-premium-static-ip-address-http-frontend"
  ip_address            = data.google_compute_global_address.premium_static_ip.address
  ip_protocol           = "TCP"
  port_range            = "80-80"
  target                = google_compute_target_http_proxy.espresso.id
  load_balancing_scheme = "EXTERNAL"
}

resource "google_compute_global_forwarding_rule" "https" {
  name                  = "espresso-premium-static-ip-address-https-frontend"
  ip_address            = data.google_compute_global_address.premium_static_ip.address
  ip_protocol           = "TCP"
  port_range            = "443-443"
  target                = google_compute_target_https_proxy.espresso.id
  load_balancing_scheme = "EXTERNAL"
}
