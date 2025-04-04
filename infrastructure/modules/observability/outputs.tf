output "MONITOR_RESOURCE_GROUP" {
  value     = azurerm_resource_group.this.name
  sensitive = false
}

output "GRAFANA_RESOURCE_ID" {
  value     = var.enable_managed_offerings ? azurerm_dashboard_grafana.this[0].id : null
  sensitive = false
}

output "AZURE_MONITOR_WORKSPACE_ID" {
  value     = var.enable_managed_offerings ? azurerm_monitor_workspace.this[0].id : null
  sensitive = false
}

output "DATA_COLLECTION_RULES_ID" {
  value     = var.enable_managed_offerings ? azurerm_monitor_data_collection_rule.azuremonitor[0].id : null
  sensitive = false
}

output "DATA_COLLECTION_RULE_CONTAINER_INSIGHTS_ID" {
  value     = var.enable_managed_offerings ? azurerm_monitor_data_collection_rule.container_insights[0].id : null
  sensitive = false
}

output "LOG_ANALYTICS_RESOURCE_ID" {
  value     = azurerm_log_analytics_workspace.this.id
  sensitive = false
}

output "APP_INSIGHTS_RESOURCE_ID" {
  value     = azurerm_application_insights.this.id
  sensitive = false
}

output "APP_INSIGHTS_CONNECTION_STRING" {
  value     = azurerm_application_insights.this.connection_string
  sensitive = true
}

output "APP_INSIGHTS_INSTRUMENTATION_KEY" {
  value     = azurerm_application_insights.this.instrumentation_key
  sensitive = true
}

output "OTEL_IDENTITY_NAME" {
  value     = azurerm_user_assigned_identity.otel_identity.name
  sensitive = false
}