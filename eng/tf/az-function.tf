resource "azurerm_app_service_plan" "afa_plan" {
  resource_group_name = azurerm_resource_group.rg.name
  name                = "plan-${var.project}"
  location            = var.location

  kind = "FunctionApp"
  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "afa_app" {
  resource_group_name = azurerm_resource_group.rg.name
  name                = "afa-${var.project}"
  location            = var.location

  app_service_plan_id  = azurerm_app_service_plan.afa_plan.id
  storage_account_name = azurerm_storage_account.storage_afa.name
  storage_account_access_key = azurerm_storage_account.storage_afa.primary_access_key

  version = "~3"

  identity {
    type = "SystemAssigned"
  }

  auth_settings {
    enabled = true
    runtime_version = "v2"
    unauthenticated_client_action = "RedirectToLoginPage"
    active_directory {
      client_id = azuread_application.ad_app.application_id
      client_secret = azuread_application_password.ad_app_secret.value
    }
  }

  app_settings = {
    "APPINSIGHTS_INSTRUMENTATIONKEY" = azurerm_application_insights.ai.instrumentation_key
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
    "WEBSITE_RUN_FROM_PACKAGE" = "1"

    "AzureAd:ClientId" = azuread_application.ad_app.application_id
    "AzureAd:ClientSecret" = azuread_application_password.ad_app_secret.value
    "AzureAd:TenantId" =  data.azurerm_client_config.current.tenant_id
  }
}
