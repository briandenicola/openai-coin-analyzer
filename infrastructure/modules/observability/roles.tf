resource "azurerm_role_assignment" "grafana_monitoring_read" {
  count                            = var.enable_managed_offerings ? 1 : 0
  scope                            = azurerm_resource_group.this.id
  role_definition_name             = "Monitoring Reader"
  principal_id                     = azurerm_dashboard_grafana.this[0].identity[0].principal_id
  skip_service_principal_aad_check = true
}

resource "azurerm_role_assignment" "grafana_monitoring_data_read" {
  count                            = var.enable_managed_offerings ? 1 : 0
  scope                            = azurerm_monitor_workspace.this[0].id
  role_definition_name             = "Monitoring Data Reader"
  principal_id                     = azurerm_dashboard_grafana.this[0].identity[0].principal_id
  skip_service_principal_aad_check = true
}

resource "azurerm_role_assignment" "grafana_admin" {
  count                            = var.enable_managed_offerings ? 1 : 0
  scope                            = azurerm_resource_group.this.id
  role_definition_name             = "Grafana Admin"
  principal_id                     = data.azurerm_client_config.current.object_id
}

resource "azurerm_role_assignment" "metric_publisher" {
  scope                            = azurerm_log_analytics_workspace.this.id
  role_definition_name             = "Monitoring Metrics Publisher"
  principal_id                     = azurerm_user_assigned_identity.otel_identity.principal_id
  skip_service_principal_aad_check = true
}

resource "azurerm_role_assignment" "app_insight_metric_publisher" {
  scope                            = azurerm_application_insights.this.id
  role_definition_name             = "Monitoring Metrics Publisher"
  principal_id                     = azurerm_user_assigned_identity.otel_identity.principal_id
  skip_service_principal_aad_check = true
}