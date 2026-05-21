# Espresso Infrastructure

Terraform configuration for the GCP project `espresso-8c4ac`.

## Layout

```
infrastructure/
├── foundation/   # rare-change resources (network, IAM, SQL, AR, static IP)
└── services/     # frequent-change resources (instance templates, MIGs, load balancer)
```

Two stacks instead of one. Foundation rarely changes; services change with deploys. Splitting them keeps `terraform apply` blast radius small — a routine compute change cannot accidentally touch Cloud SQL.

The `services` stack reads foundation resources via `data "google_*"` lookups by name. There is no `terraform_remote_state` coupling between stacks.

## Prerequisites

- `terraform` >= 1.6
- `gcloud` authenticated as a user with permission to manage the GCP project
- Default project set to `espresso-8c4ac`:
  ```sh
  gcloud config set project espresso-8c4ac
  ```

## State backend

Remote state in GCS bucket `espresso-8c4ac-tfstate` (region: `europe-west3`, uniform bucket-level access, public access prevention enforced, versioning enabled).

The bucket is bootstrapped manually via `gcloud` — it is **not** managed by Terraform (chicken-and-egg). See [Bootstrap](#bootstrap) below.

State keys:
- `gs://espresso-8c4ac-tfstate/foundation/default.tfstate`
- `gs://espresso-8c4ac-tfstate/services/default.tfstate`

## Bootstrap

One-time setup of the state bucket:

```sh
gcloud storage buckets create gs://espresso-8c4ac-tfstate \
  --project=espresso-8c4ac \
  --location=europe-west3 \
  --uniform-bucket-level-access \
  --public-access-prevention

gcloud storage buckets update gs://espresso-8c4ac-tfstate --versioning
```

## Common operations

```sh
# From either stack directory:
terraform init        # first time, or after backend/provider changes
terraform fmt         # auto-format .tf files
terraform validate    # semantic check
terraform plan        # preview changes
terraform apply       # apply changes
```

Run `terraform fmt -check -recursive` from `infrastructure/` to verify formatting across both stacks.

## What is intentionally NOT codified

These resources exist in GCP but Terraform does not manage them, because they are either auto-created by GCP/Firebase or scheduled for cleanup:

- The `default` VPC and its default firewall rules (`default-allow-http`, `default-allow-https`, `default-allow-icmp`, `default-allow-internal`, `default-allow-rdp`, `default-allow-ssh`) — auto-created by GCP, referenced via `data` lookup.
- Default service accounts (App Engine, Compute Engine) — auto-created by GCP.
- `firebase-adminsdk-*` service account — managed by Firebase.
- `espresso-app-*` resources (MIG, instance template, AR image) — legacy infrastructure, scheduled for decommissioning. See `memory/` notes.
- Zombie `k8s-fw-*` firewall rules and `espresso-cluster-service-accou` service account — leftover from a previous GKE deployment. Will be cleaned up out-of-band after the import is complete.

## Resource naming

Existing resources keep their current names — renaming on import would force destroy/recreate of live infrastructure. Names visible in GCP today are the source of truth.

## Adding a new resource

1. Decide which stack owns it (foundation if rarely-changing, services if part of deploy cycle).
2. Add the `resource` block to the relevant `.tf` file in that stack (file split by resource family; see per-stack README).
3. `terraform plan` to preview. If the resource already exists in GCP, use `terraform import` first.
4. `terraform apply`.

See each stack's `README.md` for stack-specific guidance.
