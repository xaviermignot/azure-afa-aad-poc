resource "azurerm_storage_account" "storage_afa" {
  resource_group_name = azurerm_resource_group.rg.name
  name                = "stor${lower(replace(var.project, "-", ""))}"
  location            = var.location

  account_tier             = "Standard"
  account_replication_type = "LRS"
}