resource "azurerm_storage_account" "this" {
  name                     = local.storage_account_name
  resource_group_name      = var.resource_group.name
  location                 = var.resource_group.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  account_kind             = "StorageV2"
  min_tls_version          = "TLS1_2"
}

resource "azurerm_storage_container" "this" {
  name                  = "test-data"
  storage_account_id    = azurerm_storage_account.this.id
  container_access_type = "private"
}
