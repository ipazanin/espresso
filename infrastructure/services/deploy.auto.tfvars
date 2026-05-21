# Bot-managed deploy state.
#
# This file is committed and is automatically updated by the `deploy` job in
# .github/workflows/ci-cd-workflow.yml on every tag push. Do NOT edit by hand
# unless you're rolling back — see the "Rolling back" section of README.md.
#
# Keeping these two values isolated from terraform.tfvars makes human edits
# (zones, project IDs, server URLs) easy to review without bot-commit noise
# polluting the file's blame history.

webapi_image_tag    = "2.4.8"
dashboard_image_tag = "2.4.8"
