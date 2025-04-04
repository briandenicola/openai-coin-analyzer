variable "region" {
  description = "Azure region to deploy to"
  default     = "southcentralus"
}

variable "grafana_region" {
  description = "Azure region to deploy to"
  default     = "southcentralus"
  validation {
    condition     = contains(["southcentralus", "westcentralus","eastus2", "uksouth","canadacentral"], var.grafana_region)
    error_message = "Valid values for var: grafana_region are (Windows, Linux)."
  } 
}

variable "resource_name" {
  description = "The root value to use for naming resources"
}

variable "otel_namespace" {
  description = "The namespace to use for OpenTelemetry"
}

variable "sdlc_environment" {
  description = "The value for the sdlc environment"
}

variable "tags" {
  description = "Tags to apply for this resource"
}

variable "enable_managed_offerings" {
  description = "Deploy Managed Prometheus and Managed Grafana"
  default     = true
}

variable "use_aad_authentication" {
  description = "Use AAD authentication for Azure Monitor"
  default     = true
}