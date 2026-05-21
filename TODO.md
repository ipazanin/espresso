# TODO

Time-bound follow-up tasks. One-off items only — recurring chores belong in CI or scheduled jobs.

## Cost / billing

- [ ] **2026-05-28 or later — verify measured GCP cost vs estimates.**
  - **Why:** The cost-optimization pass on 2026-05-21 (PRs `8608abad`, `badd2e79`, `bfd426f2`) projected the run-rate dropping from ~€53/mo to ~€36/mo, but every number was a list-price estimate. The BigQuery billing export starts producing usable data ~24h after wire-up, with full backfill within ~5 days. By 2026-05-28 there's enough measured data to confirm reality.
  - **How:** Run the SKU breakdown query documented in [`infrastructure/foundation/README.md` § Cloud Billing BigQuery export](infrastructure/foundation/README.md#cloud-billing-bigquery-export). Compare top SKUs against the estimates in commit `8608abad`'s analysis.
  - **What to look for:**
    - Cloud SQL backup-storage cost — forecast €0-6/mo; if it's a meaningful new line item, decide whether to keep PITR
    - Dashboard VM compute — should appear as €0 (Always Free credited). If not, free-tier eligibility broke somewhere
    - HTTPS LB forwarding rule — should drop from ~€33.58 to ~€16.79/mo after PR 2
    - Any surprise SKU > €1/mo that wasn't in the inventory
  - **If numbers diverge significantly** (>20% from estimate): re-run the analysis, update memory, possibly open a new optimization pass.
