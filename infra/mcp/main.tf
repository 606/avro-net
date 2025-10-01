terraform {
  required_providers {
    github = {
      source  = "integrations/github"
      version = "~> 5.0"
    }
  }
}

provider "github" {
  token = var.github_token
  owner = var.github_owner
}

# Configure repository secrets for MCP server
resource "github_actions_secret" "mcp_secrets" {
  for_each = {
    ASPNETCORE_ENVIRONMENT = "Production"
    MCP_API_KEY           = var.mcp_api_key
  }

  repository      = var.repository_name
  secret_name     = each.key
  encrypted_value = each.value
}

# Set up branch protection specific to MCP
resource "github_branch_protection_v3" "mcp_protection" {
  repository     = var.repository_name
  branch         = "main"
  enforce_admins = true

  required_status_checks {
    strict = true
    contexts = ["build", "test", "terraform"]
  }

  required_pull_request_reviews {
    required_approving_review_count = 1
    dismiss_stale_reviews          = true
  }

  restrictions = null
}