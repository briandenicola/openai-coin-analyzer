module "cluster" {
  depends_on = [
    module.azure_monitor
  ]
  source     = "./aks"
  resource_name = local.resource_name
  aks_cluster = {
    name = local.aks_name
    authorized_ip_ranges = ["${chomp(data.http.myip.response_body)}/32"]
    public_key_openssh = tls_private_key.rsa.public_key_openssh
    zones = [ "1", "2", "3" ]
    resource_group = {
      name = azurerm_resource_group.this.name
      id   = azurerm_resource_group.this.id
    }
    location = azurerm_resource_group.this.location
    version  = local.kubernetes_version
    istio = {
      version = local.istio_version
    }
    nodes = {
      sku   = var.node_sku
      count = var.node_count
    }
    vnet = {
      id = azurerm_virtual_network.this.id
      node_subnet = {
        id = azurerm_subnet.nodes.id
      }
    }
    logs = {
      workspace_id = module.azure_monitor.LOG_ANALYTICS_RESOURCE_ID
    }
    flux = {
      enabled    = true
      repository = local.flux_repository
      app_path   = local.app_path
    }
  }
}