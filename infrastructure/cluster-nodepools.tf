resource "azurerm_kubernetes_cluster_node_pool" "app_node_pool" {
  depends_on = [
    azapi_update_resource.cluster_updates
  ]

  lifecycle {
    ignore_changes = [
      node_count
    ]
  }

  name                  = "ric"
  kubernetes_cluster_id = azurerm_kubernetes_cluster.this.id
  vnet_subnet_id        = azurerm_subnet.kubernetes.id
  vm_size               = "Standard_B4ms"
  enable_auto_scaling   = true
  mode                  = "User"
  os_sku                = "Mariner"
  os_disk_type          = "Ephemeral"
  os_disk_size_gb       = 127
  max_pods              = 250
  node_count            = var.node_count
  min_count             = var.node_count
  max_count             = var.node_count + 2

  upgrade_settings {
    max_surge = "25%"
  }

  node_taints           = [ "app=ric:NoSchedule" ]
}