variable "region" {
  description = "The location for this application deployment"
  default     = "southcentralus"
}

variable "tags" {
  description = "Tags to apply to Resource Group"
}

variable "namespace" {
  description = "The namespace application will be deployed to"
  default     = "ric-analyzer"
}

variable "vm_size" {
  description = "The SKU for the default node pool"
  default     = "Standard_B4ms"
}

variable "node_count" {
  description = "The default number of nodes to scale the cluster to"
  type        = number
  default     = 1
}

variable "postgresql_user_name" {
  description = "Azure PostgreSQL User Name"
  type        = string
  default     = "manager"
}

variable "postgresql_database_name" {
  description = "PostgreSQL Database Name"
  type        = string
  default     = "coinsdb"
}

variable "github_repo_branch" {
  description = "The branched used for InfrastructureGitOps"
  default     = "main"
}