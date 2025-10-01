output "repository_name" {
  value = github_repository.sdk.full_name
}

output "repository_url" {
  value = github_repository.sdk.html_url
}

output "packages_url" {
  value = "${github_repository.sdk.html_url}/packages"
}