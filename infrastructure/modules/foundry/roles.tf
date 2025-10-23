resource "azurerm_role_assignment" "ai_services_user" {
  scope                            = azapi_resource.foundry.id
  role_definition_name             = "Cognitive Services OpenAI User"
  principal_id                     = data.azurerm_client_config.current.object_id
}

resource "azurerm_role_assignment" "storage_owner" {
  scope                            = azurerm_storage_account.this.id
  role_definition_name             = "Storage Blob Data Owner"
  principal_id                     = data.azurerm_client_config.current.object_id
  depends_on                      = [azurerm_storage_account.this]
}

resource "azurerm_role_assignment" "storage_contributor_msi" {
  scope                            = azurerm_storage_account.this.id
  role_definition_name             = "Storage Blob Data Contributor"
  principal_id                     = azurerm_user_assigned_identity.foundry_identity.principal_id
  depends_on                      = [azurerm_storage_account.this]
}

resource "azurerm_role_assignment" "foundry_storage_access" {
  scope                            = azurerm_storage_account.this.id
  role_definition_name             = "Storage Blob Data Contributor"
  principal_id                     = azapi_resource.foundry.output.identity.principalId
}

resource "azurerm_role_assignment" "foundry_search_contributor_access" {
  scope                            = azapi_resource.ai_search.id
  role_definition_name             = "Search Service Contributor"
  principal_id                     = azapi_resource.foundry.output.identity.principalId
}

resource "azurerm_role_assignment" "foundry_search_index_access" {
  scope                            = azapi_resource.ai_search.id
  role_definition_name             = "Search Index Data Reader"
  principal_id                     = azapi_resource.foundry.output.identity.principalId
}

resource "azurerm_role_assignment" "search_storage_access" {
  scope                            = azurerm_storage_account.this.id
  role_definition_name             = "Storage Blob Data Reader"
  principal_id                     = azapi_resource.ai_search.output.identity.principalId
}

resource "azurerm_role_assignment" "search_openai_access" {
  scope                            =  azapi_resource.foundry.id
  role_definition_name             = "Cognitive Services OpenAI Contributor"
  principal_id                     = azapi_resource.ai_search.output.identity.principalId
}