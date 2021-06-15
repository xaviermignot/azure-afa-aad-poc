terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "2.63.0"
    }

    azuread = {
      source = "hashicorp/azuread"
      version = "1.5.1"
    }
  }
}

provider "azurerm" {
  features {}
}