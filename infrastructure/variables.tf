variable "region" {
  description = "The location for this application deployment"
  default     = "southcentralus"
}

variable "tags" {
  description = "Tags to apply to Resource Group"
}

variable "namespace" {
  description = "The namespace application will be deployed to"
  default     = "ric"
}

variable "node_sku" {
  description = "The SKU for the default node pool"
  default     = "Standard_B4ms"
}

variable "node_count" {
  description = "The default number of nodes to scale the cluster to"
  type        = number
  default     = 1
}

variable "certificate_base64_encoded" {
  description = "TLS Certificate for Istio Ingress Gateway"
}

variable "certificate_password" {
  description = "Password for TLS Certificate"
}

variable "certificate_name" {
  description = "The name of the certificate to use for TLS"
  default     = "wildcard-certificate"
}

variable "custom_domain" {
  description = "The custom domain to use for the application"
}