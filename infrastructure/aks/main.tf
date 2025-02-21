data "azurerm_client_config" "current" {}
data "azurerm_subscription" "current" {}

resource "random_integer" "services_cidr" {
  min = 64
  max = 99
}

resource "random_integer" "pod_cidr" {
  min = 100
  max = 127
}

locals {
  location             =  var.aks_cluster.location
  aks_name             =  var.aks_cluster.name
  aks_node_rg_name     = "${local.aks_name}_nodes_rg"
  istio_version        = [ var.aks_cluster.istio.version ]
}

data "azurerm_resource_group" "this" {
  name = var.aks_cluster.resource_group.name
}