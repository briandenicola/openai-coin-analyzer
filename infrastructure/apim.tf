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
  description           = "Roman Imperial Coin Analyzer"
  subscription_required = true
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
  path                  = local.apim_api_path
  protocols             = ["http", "https"]
  subscription_required = true
  service_url           = var.custom_domain

  #
  # There is a bug in the azurerm provider that causes the API to not be created using "swagger-link-json"
  # Manually update the API to import the Swagger document from ${local.swagger_url} in the Azure portal after creation
  #
  # import {
  #   content_format = "swagger-link-json"
  #   content_value  = local.swagger_url
  # }
  #
}

resource "azurerm_api_management_product_api" "ric_api_product_association" {
  api_name            = azurerm_api_management_api.ric_api.name
  product_id          = azurerm_api_management_product.ric_api_product.product_id
  api_management_name = azurerm_api_management.this.name
  resource_group_name = azurerm_api_management.this.resource_group_name
}

resource "azurerm_api_management_backend" "ric_api_backend" {
  name                = local.apim_backend_name
  resource_group_name = azurerm_resource_group.app.name
  api_management_name = azurerm_api_management.this.name
  protocol            = "http"
  url                 = var.custom_domain
}

resource "azurerm_api_management_subscription" "ric_ui_subscription" {
  api_management_name = azurerm_api_management.this.name
  resource_group_name = azurerm_api_management.this.resource_group_name
  product_id          = azurerm_api_management_product.ric_api_product.id
  display_name        = "Roman Imperial Coin Analyzer UI Subscription"
  allow_tracing       = true
  state               = "active"
}

resource "azurerm_api_management_logger" "this" {
  name                = "${local.apim_name}-logger"
  api_management_name = azurerm_api_management.this.name
  resource_group_name = azurerm_api_management.this.resource_group_name

  application_insights {
    connection_string = module.azure_monitor.APP_INSIGHTS_CONNECTION_STRING
  }
}

resource "azurerm_api_management_api_policy" "ric_ui_api_policy" {
  api_name            = azurerm_api_management_api.ric_api.name
  api_management_name = azurerm_api_management.this.name
  resource_group_name = azurerm_api_management.this.resource_group_name

  xml_content = <<XML
    <policies>
      <inbound>
          <base />
          <cors allow-credentials="false">
              <allowed-origins>
                  <origin>https://${azurerm_static_web_app.this.default_host_name}</origin>
              </allowed-origins>
              <allowed-methods>
                  <method>GET</method>
                  <method>POST</method>
                  <method>PUT</method>
                  <method>DELETE</method>
                  <method>HEAD</method>
                  <method>OPTIONS</method>
              </allowed-methods>
              <allowed-headers>
                  <header>*</header>
              </allowed-headers>
          </cors>
      </inbound>
      <backend>
          <base />
      </backend>
      <outbound>
          <base />
      </outbound>
      <on-error>
          <base />
      </on-error>
    </policies>
XML

}
