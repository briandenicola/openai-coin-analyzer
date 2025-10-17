output "APP_NAME" {
  value     = local.resource_name
  sensitive = false
}

output "APP_RESOURCE_GROUP" {
    value = azurerm_resource_group.app.name
    sensitive = false
}

output "AZURE_AI_PROJECT_ENDPOINT" {
    value = module.foundry.AZURE_AI_PROJECT_ENDPOINT
    sensitive = false
}
