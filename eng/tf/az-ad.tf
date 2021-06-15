resource "azuread_application" "ad_app" {
  display_name = "APP-${upper(var.project)}"
  identifier_uris = ["api://${var.project}"]
}

resource "azuread_application_password" "ad_app_secret" {
    application_object_id = azuread_application.ad_app.object_id
    display_name = "LocalDevSecret"
}

output "out_ad_app_secret_value" {
  value = azuread_application_password.ad_app_secret.value
  sensitive = true
}