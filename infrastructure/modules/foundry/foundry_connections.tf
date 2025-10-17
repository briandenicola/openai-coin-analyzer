resource "azapi_resource" "ai_search_connection" {
  type      = "Microsoft.CognitiveServices/accounts/projects/connections@2025-06-01"
  name      = local.ai_search_connection_name
  parent_id = azapi_resource.foundry_project.id

  body = {
    properties = {
      category      = "CognitiveSearch"
      authType      = "ApiKey"
      isSharedToAll = true
      metadata = {
        ApiType    = "Azure"
        ResourceId = azapi_resource.ai_search.id
        type       = "ai_search"
      }
      target = azapi_resource.ai_search.output.properties.endpoint
      credentials = {
        key = data.azapi_resource_action.search_keys.output.primaryKey
      }
    }
  }
}

resource "azapi_resource" "storage_connection" {
  depends_on = [azapi_resource.ai_search_connection, ]
  type       = "Microsoft.CognitiveServices/accounts/projects/connections@2025-06-01"

  name      = local.storage_connection_name
  parent_id = azapi_resource.foundry_project.id

  body = {
    properties = {
      category      = "AzureBlob"
      authType      = "ManagedIdentity"
      isSharedToAll = true
      metadata = {
        ApiType       = "Azure"
        ResourceId    = azurerm_storage_account.this.id
        type          = "azure_storage"
        ContainerName = azurerm_storage_container.this.name
        AccountName   = azurerm_storage_account.this.name
      }
      target = azurerm_storage_account.this.primary_blob_endpoint
      credentials = {
        clientId   = azurerm_user_assigned_identity.foundry_identity.client_id
        resourceId = azurerm_user_assigned_identity.foundry_identity.id
      }
    }
  }
}


resource "azapi_resource" "app_insights_connection" {
  count      = var.log_analytics.deploy ? 1 : 0
  depends_on = [azapi_resource.storage_connection]
  type       = "Microsoft.CognitiveServices/accounts/projects/connections@2025-06-01"

  name      = local.app_insights_connection_name
  parent_id = azapi_resource.foundry_project.id

  body = {
    properties = {
      category      = "AppInsights"
      authType      = "ApiKey"
      isSharedToAll = true
      metadata = {
        ApiType    = "Azure"
        ResourceId = var.log_analytics.application_insights.id
        type       = "azure_app_insights"
      }
      target = var.log_analytics.application_insights.id
      credentials = {
        key = var.log_analytics.application_insights.connection_string
      }
    }
  }
}

