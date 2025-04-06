resource "azurerm_user_assigned_identity" "otel_identity" {
  name                = "${local.workload_identity}"
  resource_group_name = azurerm_resource_group.this.name
  location            = azurerm_resource_group.this.location
}