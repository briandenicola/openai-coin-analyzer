locals {
  resource_name          = "${random_pet.this.id}-${random_id.this.dec}"
  aks_name               = "${local.resource_name}-aks"
  kv_name                = "${local.resource_name}-kv"
  nat_name               = "${local.resource_name}-nat"
  vnet_name              = "${local.resource_name}-vnet"
  sa_name                = "${replace(local.resource_name, "-", "")}files"
  acr_name               = "${replace(local.resource_name, "-", "")}acr"
  static_webapp_name     = "${local.resource_name}-ui"
  static_webapp_location = "centralus"
  sql_name               = "${local.resource_name}-sql"
  app_path               = "./cluster-config"
  flux_repository        = "https://github.com/briandenicola/openai-coin-analyzer"
  environment_type       = "dev"

  vnet_cidr               = cidrsubnet("10.0.0.0/8", 8, random_integer.vnet_cidr.result)
  pe_subnet_cidir         = cidrsubnet(local.vnet_cidr, 8, 1)
  api_subnet_cidir        = cidrsubnet(local.vnet_cidr, 8, 2)
  nodes_subnet_cidir      = cidrsubnet(local.vnet_cidr, 8, 3)
  compute_subnet_cidir    = cidrsubnet(local.vnet_cidr, 8, 4)
  sql_subnet_cidir        = cidrsubnet(local.vnet_cidr, 8, 10)

  kubernetes_version      = "1.30"
  istio_version           = "asm-1-23"
}