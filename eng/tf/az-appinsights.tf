resource "azurerm_application_insights" "ai" {
  resource_group_name = azurerm_resource_group.rg.name
  name = "ai-${var.project}"
  location = var.location

  application_type = "web"
}