resource "azurerm_api_management" "example" {
  name                = local.apim_name
  location            = azurerm_resource_group.this.location
  resource_group_name = azurerm_resource_group.this.name
  publisher_name      = "BD"
  publisher_email     = "admin@bjdazure.tech"
  sku_name            = "Consumption"
}
