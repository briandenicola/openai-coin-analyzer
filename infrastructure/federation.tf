resource "azurerm_federated_identity_credential" "app_identity" {
  name                = "${local.workload_identity}"
  resource_group_name = azurerm_resource_group.app.name
  audience            = ["api://AzureADTokenExchange"]
  issuer              = data.azurerm_kubernetes_cluster.this.oidc_issuer_url
  parent_id           = azurerm_user_assigned_identity.app_identity.id
  subject             = "system:serviceaccount:${var.namespace}:${local.workload_identity}"
}

resource "azurerm_federated_identity_credential" "otel_identity" {
  depends_on = [
    module.azure_monitor,
    module.cluster
  ]  
  name                = "${local.workload_identity}"
  resource_group_name = module.azure_monitor.MONITOR_RESOURCE_GROUP
  audience            = ["api://AzureADTokenExchange"]
  issuer              = data.azurerm_kubernetes_cluster.this.oidc_issuer_url
  parent_id           = data.azurerm_user_assigned_identity.otel_identity.id
  subject             = "system:serviceaccount:${local.otel_namespace}:${module.azure_monitor.OTEL_IDENTITY_NAME}"
}

