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

# Configure NuGet package publishing secrets
resource "github_actions_secret" "nuget_secrets" {
  for_each = {
    NUGET_API_KEY = var.nuget_api_key
    GITHUB_TOKEN  = var.github_token
  }

  repository      = var.repository_name
  secret_name     = each.key
  encrypted_value = each.value
}

# Configure repository settings
resource "github_repository" "sdk" {
  name        = var.repository_name
  description = "Avro.NET SDK"
  visibility  = "public"

  has_issues      = true
  has_discussions = true
  has_wiki        = true
  has_downloads   = true

  allow_merge_commit = true
  allow_squash_merge = true
  allow_rebase_merge = true

  topics = ["avro", "dotnet", "sdk", "operating-system"]
}

# Set up branch protection for main branch
resource "github_branch_protection" "main" {
  repository_id = github_repository.sdk.name
  pattern       = "main"

  required_status_checks {
    strict = true
    contexts = ["build", "test"]
  }

  required_pull_request_reviews {
    required_approving_review_count = 1
    dismiss_stale_reviews          = true
  }
}