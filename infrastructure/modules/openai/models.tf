resource "azurerm_cognitive_deployment" "gpt" {
  name                 = "gpt-4o"
  cognitive_account_id = azurerm_cognitive_account.this.id
  model {
    format  = "OpenAI"
    name    = "gpt-4o"
    version = "2024-05-13"
  }

  sku {
    name = "Standard"
  }
}

resource "azurerm_cognitive_deployment" "gpt4_turbo" {
  name                 = "gpt-4-turbo"
  cognitive_account_id = azurerm_cognitive_account.this.id
  model {
    format  = "OpenAI"
    name    = "gpt-4"
    version = "vision-preview"
  }

  sku {
    name     = "Standard"
    capacity = 10
  }
}
