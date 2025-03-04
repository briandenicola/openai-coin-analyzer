
resource "azurerm_kubernetes_cluster_extension" "dapr" {
  depends_on = [
    azurerm_kubernetes_cluster_extension.flux,
  ]
  name              = "dapr"
  cluster_id        = azurerm_kubernetes_cluster.this.id
  extension_type    = "microsoft.dapr"
  release_namespace = "dapr-system"
}

resource "azurerm_kubernetes_cluster_extension" "flux" {
  depends_on = [
    azapi_update_resource.cluster_updates
  ]
  name           = "flux"
  cluster_id     = azurerm_kubernetes_cluster.this.id
  extension_type = "microsoft.flux"
}

resource "azurerm_kubernetes_flux_configuration" "flux_config" {
  depends_on = [
    azurerm_kubernetes_cluster_extension.flux
  ]

  name       = "aks-flux-extension"
  cluster_id = azurerm_kubernetes_cluster.this.id
  namespace  = "flux-system"
  scope      = "cluster"

  git_repository {
    url                      = var.aks_cluster.flux.repository
    reference_type           = "branch"
    reference_value          = var.aks_cluster.flux.branch
    timeout_in_seconds       = 600
    sync_interval_in_seconds = 30
  }

  kustomizations {
    name = "cluster-config"
    path = var.aks_cluster.flux.app_path

    timeout_in_seconds         = 600
    sync_interval_in_seconds   = 120
    retry_interval_in_seconds  = 300
    garbage_collection_enabled = true
  }
}