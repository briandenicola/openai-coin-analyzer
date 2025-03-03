resource "azurerm_container_registry" "this" {
  name                     = local.acr_name
  resource_group_name      = azurerm_resource_group.this.name
  location                 = azurerm_resource_group.this.location
  sku                      = "Premium"
  admin_enabled            = false
  data_endpoint_enabled    = true 
  anonymous_pull_enabled   = true

  network_rule_set {
    default_action = "Allow"
    ip_rule {
      ip_range   = var.aks_cluster.authorized_ip_ranges
      action     = "Allow"
    }
  }
}
