variable "resource_name" {
  description = "The root value to use for naming resources"
}

variable "aks_cluster" {
  type = object({
    name                  = string
    location              = string
    authorized_ip_ranges  = list(string)
    public_key_openssh    = string
    zones                 = list(string)
    resource_group = object({
      name = string
      id   = string
    })
    version = string
    istio = object({
      version = string
    })
    nodes = object({
      sku   = string
      count = number
    })
    vnet = object({
      id = string
      node_subnet = object({
        id = string
      })
    })
    logs = object({
      workspace_id = string
    })
    flux = object({
      enabled    = bool
      repository = string
      app_path   = string
    })
  })
}
