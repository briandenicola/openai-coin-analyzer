module "foundry" {
  depends_on = [ 
    azurerm_resource_group.app,
  ]
  source               = "../modules/foundry"
  resource_name        = local.resource_name
  
  resource_group = {
    location = azurerm_resource_group.app.location
    name     = azurerm_resource_group.app.name
    id       = azurerm_resource_group.app.id
  }

  log_analytics ={ 
    deploy = false
    workspace_id = null
    application_insights = {
      id                = null
      connection_string = null
    }
  }
}