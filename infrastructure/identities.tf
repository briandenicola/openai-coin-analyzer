resource "azurerm_user_assigned_identity" "app_identity" {
  name                = "${local.workload_identity}"
  resource_group_name = azurerm_resource_group.app.name
  location            = azurerm_resource_group.app.location
}

data "azurerm_user_assigned_identity" "otel_identity" {
   depends_on = [
    module.azure_monitor
  ]   
  name                = module.azure_monitor.OTEL_IDENTITY_NAME
  resource_group_name = module.azure_monitor.MONITOR_RESOURCE_GROUP
}
