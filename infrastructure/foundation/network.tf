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

