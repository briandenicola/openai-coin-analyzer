output "APP_NAME" {
  value     = local.resource_name
  sensitive = false
}

output "APP_RESOURCE_GROUP" {
    value = azurerm_resource_group.app.name
    sensitive = false
}

output "AKS_CLUSTER_NAME" {
    value = module.cluster.AKS_CLUSTER_NAME
    sensitive = false
}

output "AKS_RESOURCE_GROUP" {
    value = module.cluster.AKS_RESOURCE_GROUP
    sensitive = false
}

output "ACR_NAME" {
  value     = local.acr_name
  sensitive = false
}

output "OPENAI_ENDPOINT" {
    value = module.openai.OPENAI_ENDPOINT
    sensitive = false
}

output "WORKLOAD_ID_NAME" {
    value = azurerm_user_assigned_identity.app_identity.name
    sensitive = false
}

output "WORKLOAD_CLIENT_ID" {
    value = azurerm_user_assigned_identity.app_identity.client_id
    sensitive = false
}

output "WORKLOAD_TENANT_ID" {
    value = azurerm_user_assigned_identity.app_identity.tenant_id
    sensitive = false
}

output "APP_INSIGHTS_CONNECTION_STRING" {
    value = module.azure_monitor.APP_INSIGHTS_CONNECTION_STRING
    sensitive = true
}

output "APP_INSIGHTS_INSTRUMENTATION_KEY" {
    value = module.azure_monitor.APP_INSIGHTS_INSTRUMENTATION_KEY
    sensitive = true
}

output "AZURE_STATIC_WEBAPP_NAME" {
    value = azurerm_static_web_app.this.name
    sensitive = false
}

output "APIM_GATEWAY" {
    value = local.app_apim_gateway_url
    sensitive = false
}

output "APIM_SUBSCRIPTION_ID" {
    value = azurerm_api_management_subscription.ric_ui_subscription.primary_key
    sensitive = true
}

output "ISTIO_CLIENT_ID" {
    value = module.cluster.ISTIO_CLIENT_ID
    sensitive = false
}

output "ISTIO_TENANT_ID" {
    value = module.cluster.ISTIO_TENANT_ID
    sensitive = false
}

output "KEYVAULT_NAME" {
    value = module.cluster.KEYVAULT_NAME
    sensitive = false
}

output "OTEL_WORKLOAD_ID_NAME" {
    value = data.azurerm_user_assigned_identity.otel_identity.name
    sensitive = false
}
output "OTEL_CLIENT_ID" {
    value = data.azurerm_user_assigned_identity.otel_identity.client_id
    sensitive = false
}