data "http" "myip" {
  url = "http://checkip.amazonaws.com/"
}

resource "random_id" "this" {
  byte_length = 2
}

resource "random_pet" "this" {
  length    = 1
  separator = ""
}

resource "random_password" "password" {
  length  = 25
  special = true
}

resource "random_integer" "vnet_cidr" {
  min = 10
  max = 250
}

resource "random_integer" "services_cidr" {
  min = 64
  max = 99
}

resource "random_integer" "pod_cidr" {
  min = 100
  max = 127
}

locals {
  resource_name         = "${random_pet.this.id}-${random_id.this.dec}"
  aks_name              = "${local.resource_name}-aks"
  kv_name               = "${local.resource_name}-kv"
  ai_name               = "${local.resource_name}-ai"
  la_name               = "${local.resource_name}-logs"
  vnet_name             = "${local.resource_name}-vnet"
  sa_name               = "${replace(local.resource_name,"-","")}files"
  static_webapp_name    = "${local.resource_name}-ui"
  sql_name              = "${local.resource_name}-sql"
  openai_name           = "${local.resource_name}-openai"
  app_path              = "./cluster-config"
  flux_repository       = "https://github.com/briandenicola/openai-coin-analyzer"
  vnet_cidr             = cidrsubnet("10.0.0.0/8", 8, random_integer.vnet_cidr.result)
  pe_subnet_cidr        = cidrsubnet(local.vnet_cidr, 8, 1)
  sql_subnet_cidr       = cidrsubnet(local.vnet_cidr, 8, 2)
  api_subnet_cidir      = cidrsubnet(local.vnet_cidr, 8, 3)
  k8s_subnet_cidr       = cidrsubnet(local.vnet_cidr, 8, 4)
}