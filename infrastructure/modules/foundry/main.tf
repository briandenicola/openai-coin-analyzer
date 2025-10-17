resource "random_uuid" "guid" {}

locals {
  resource_name                = var.resource_name
  ai_foundry_name              = "${local.resource_name}-foundry"
  project_name                 = "${local.resource_name}-project"
  storage_account_name         = "${substr(replace(random_uuid.guid.result, "-", ""), 0, 22)}sa"
  search_service_name          = "${local.resource_name}-search"
  ai_search_connection_name    = "search-connection"
  app_insights_connection_name = "app-insights-connection"
  storage_connection_name      = "storage-connection"
}
