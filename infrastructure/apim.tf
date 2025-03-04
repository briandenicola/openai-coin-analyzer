resource "azurerm_api_management" "this" {
  name                = local.apim_name
  location            = azurerm_resource_group.app.location
  resource_group_name = azurerm_resource_group.app.name
  publisher_name      = "BD"
  publisher_email     = "admin@bjdazure.tech"
  sku_name            = "Consumption_0"
}

resource "azurerm_api_management_product" "ric_api_product" {
  product_id            = "ric-api-product"
  api_management_name   = azurerm_api_management.this.name
  resource_group_name   = azurerm_resource_group.app.name
  display_name          = "Roman Imperial Coin Analyzer"
  subscription_required = false
  approval_required     = false
  published             = true
}

resource "azurerm_api_management_api" "ric_api" {
  name                  = "ric-api"
  resource_group_name   = azurerm_resource_group.app.name
  api_management_name   = azurerm_api_management.this.name
  api_type              = "http"
  revision              = "1"
  display_name          = "Roman Imperial Coin Analyzer"
  path                  = "ric-api"
  protocols             = ["http", "https"]
  subscription_required = false

  #
  # There is a bug in the azurerm provider that causes the API to not be created using "swagger-link-json"
  # Manually update the API to import the Swagger document from ${local.swagger_url} in the Azure portal after creation
  #
  # import {
  #   content_format = "swagger-link-json"
  #   content_value  = local.swagger_url
  # }
}

resource "azurerm_api_management_product_api" "ric_api_product_association" {
  api_name            = azurerm_api_management_api.ric_api.name
  product_id          = azurerm_api_management_product.ric_api_product.product_id
  api_management_name = azurerm_api_management.this.name
  resource_group_name = azurerm_api_management.this.resource_group_name
}

resource "azurerm_api_management_backend" "ric_api_backend" {
  name                = "ric-backend"
  resource_group_name = azurerm_resource_group.app.name
  api_management_name = azurerm_api_management.this.name
  protocol            = "http"
  url                 = "https://api.ric.bjdazure.tech"
}
