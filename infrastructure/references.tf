data "http" "myip" {
  url = "http://checkip.amazonaws.com/"
}

data "azurerm_kubernetes_cluster" "this" {
  depends_on          = [module.cluster]
  name                = module.cluster.AKS_CLUSTER_NAME
  resource_group_name = module.cluster.AKS_RESOURCE_GROUP
}
