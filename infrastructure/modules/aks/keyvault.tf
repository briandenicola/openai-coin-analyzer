resource "azurerm_key_vault" "this" {
  name                       = local.keyvault_name
  resource_group_name        = azurerm_resource_group.this.name
  location                   = azurerm_resource_group.this.location
  tenant_id                  = data.azurerm_client_config.current.tenant_id
  soft_delete_retention_days = 7
  purge_protection_enabled   = false
  enable_rbac_authorization  = true

  sku_name = "standard"
}

resource "azurerm_key_vault_certificate" "this" {
  name         = var.aks_cluster.certificate.name 
  key_vault_id = azurerm_key_vault.this.id

  depends_on = [
    azurerm_role_assignment.deployer_kv_access
  ]

  certificate {
    contents = var.aks_cluster.certificate.contents
    password = var.aks_cluster.certificate.password
  }

  certificate_policy {
    issuer_parameters {
      name = "Self"
    }

    key_properties {
      exportable = true
      key_size   = 256
      key_type   = "EC"
      reuse_key  = false
      curve      = "P-256"
    }

    secret_properties {
      content_type = "application/x-pkcs12"
    }
  }
}