output "configured_secrets" {
  value = [for secret in github_actions_secret.mcp_secrets : secret.secret_name]
  description = "List of configured MCP secrets"
}

output "branch_protection_status" {
  value       = github_branch_protection.mcp_protection.enforce_admins
  description = "Status of branch protection rules"
}