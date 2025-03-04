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
  location                  = var.aks_cluster.location
  aks_name                  = var.aks_cluster.name
  acr_name                  = "${replace(var.resource_name, "-", "")}acr"
  keyvault_name             = "${replace(var.resource_name, "-", "")}kv"
  aks_rg_name               = "${local.aks_name}_rg"
  aks_node_rg_name          = "${local.aks_name}_nodes_rg"
  istio_version             = [var.aks_cluster.istio.version]
  aks_service_mesh_identity = "${local.aks_name}-istio-mesh-identity"
}

resource "azurerm_resource_group" "this" {
  name     = local.aks_rg_name
  location = local.location

  tags = {
    Application = var.tags
    Components  = "Azure Kubernetes Service; Azure Container Registry"
    Environment = var.sdlc_environment
    DeployedOn  = timestamp()
  }
}
