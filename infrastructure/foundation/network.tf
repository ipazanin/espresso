# Networking primitives for the foundation stack.
#
# NOT managed here:
#   - default VPC (auto-created by GCP; referenced from services via data source)
#   - default-allow-* firewall rules (auto-created by GCP)

resource "google_compute_global_address" "premium_static_ip" {
  name         = "espresso-premium-static-ip-address"
  address_type = "EXTERNAL"
  ip_version   = "IPV4"

  lifecycle {
    prevent_destroy = true
  }
}

# Allows TCP/3000 ingress to VMs tagged with `allow-3000`. Source range includes
# 0.0.0.0/0 (public) plus a private CIDR — the public rule already covers the
# private one. Created during the espresso-app-era; likely dead now that the
# React frontend is bundled into webapi behind the load balancer and the
# espresso-app MIG has been decommissioned. Codifying as-is — flag for retirement.
resource "google_compute_firewall" "allow_3000" {
  name      = "allow-3000"
  network   = "default"
  direction = "INGRESS"
  priority  = 2000

  allow {
    protocol = "tcp"
    ports    = ["3000"]
  }

  source_ranges = ["0.0.0.0/0", "192.168.2.0/24"]
  target_tags   = ["allow-3000"]
}
