module "azure_monitor" {
  depends_on = [
    azurerm_resource_group.this,
  ]
  source           = "./modules/observability"
  region           = var.region
  resource_name    = local.resource_name
  tags             = var.tags
  sdlc_environment = local.environment_type
  otel_namespace   = local.otel_namespace
}

resource "azurerm_monitor_data_collection_rule_association" "this" {
  depends_on = [
    module.azure_monitor,
    module.cluster
  ]
  name                    = "${local.resource_name}-ama-datacollection-rules-association"
  target_resource_id      = module.cluster.AKS_CLUSTER_ID
  data_collection_rule_id = module.azure_monitor.DATA_COLLECTION_RULES_ID
}

resource "azurerm_monitor_data_collection_rule_association" "container_insights" {
  name                    = "${local.resource_name}-ama-container-insights-rules-association"
  target_resource_id      = module.cluster.AKS_CLUSTER_ID
  data_collection_rule_id = module.azure_monitor.DATA_COLLECTION_RULE_CONTAINER_INSIGHTS_ID
}

data "azurerm_user_assigned_identity" "otel_identity" {
   depends_on = [
    module.azure_monitor
  ]   
  name                = module.azure_monitor.OTEL_IDENTITY_NAME
  resource_group_name = module.azure_monitor.MONITOR_RESOURCE_GROUP
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