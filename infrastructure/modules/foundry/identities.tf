resource "azurerm_user_assigned_identity" "foundry_identity" {
  name                = "${local.ai_foundry_name}-identity"
  resource_group_name = var.resource_group.name
  location            = var.resource_group.location
}
