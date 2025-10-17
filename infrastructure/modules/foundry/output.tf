output "AZURE_AI_PROJECT_ENDPOINT" {
  value     = "https://${local.ai_foundry_name}.services.ai.azure.com/api/projects/${local.project_name}"
  sensitive = false
}

output "MODEL_DEPLOYMENT_NAME" {
  value     = azurerm_cognitive_deployment.gpt_41.name
  sensitive = false
}


output "FOUNDRY_RESOURCE_ID" {
  value     = data.azurerm_cognitive_account.this.id
  sensitive = false
}

output "FOUNDRY_RESOURCE_NAME" {
  value     = data.azurerm_cognitive_account.this.name
  sensitive = false
}