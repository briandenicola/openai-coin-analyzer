resource "azurerm_log_analytics_workspace" "this" {
  name                          = "${var.resource_name}-logs"
  location                      = azurerm_resource_group.this.location
  resource_group_name           = azurerm_resource_group.this.name
  sku                           = "PerGB2018"
  daily_quota_gb                = 5
  local_authentication_disabled = var.use_aad_authentication
}

resource "azurerm_application_insights" "this" {
  name                          = "${var.resource_name}-appinsights"
  location                      = azurerm_resource_group.this.location
  resource_group_name           = azurerm_resource_group.this.name
  workspace_id                  = azurerm_log_analytics_workspace.this.id
  application_type              = "web"
  local_authentication_disabled = var.use_aad_authentication
}
