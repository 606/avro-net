variable "github_token" {
  description = "GitHub personal access token"
  type        = string
  sensitive   = true
}

variable "github_owner" {
  description = "GitHub organization or user name"
  type        = string
  default     = "606"
}

variable "repository_name" {
  description = "GitHub repository name"
  type        = string
  default     = "avro-net"
}

variable "mcp_api_key" {
  description = "API key for the MCP server"
  type        = string
  sensitive   = true
}