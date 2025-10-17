resource "azurerm_cognitive_deployment" "gpt_41" {
  name                 = "gpt-4.1"
  cognitive_account_id = data.azurerm_cognitive_account.this.id

  model {
    format  = "OpenAI"
    name    = "gpt-4.1"
  }

  sku {
    name     = "GlobalStandard"
    capacity = 250
  }
}