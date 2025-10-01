# Avro AWS Authentication

AWS authentication and credential management for .NET applications. This package provides a flexible way to manage AWS credentials and configuration in your .NET applications.

## Features

- Environment variable support for credentials (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`, `AWS_SESSION_TOKEN`)
- Region management with fallback (`AWS_REGION`, `AWS_DEFAULT_REGION`)
- IAM role support via `AWS_ROLE_ARN`
- GitHub Actions OIDC integration
- Dependency injection support
- Async operations
- Comprehensive XML documentation

## Installation

```shell
dotnet add package Avro.Aws.Auth
```

## Usage

```csharp
using Avro.Aws.Auth;
using Avro.Aws.Abstractions;

// Basic usage with environment variables
var authManager = new AwsAuthManager();
string accessKeyId = await authManager.GetAccessKeyIdAsync();
string region = await authManager.GetRegionAsync();

// Custom credential provider
public class CustomCredentialProvider : IAwsCredentialProvider
{
    public Task<string> GetAccessKeyIdAsync() => Task.FromResult("your-key");
    public Task<string> GetSecretAccessKeyAsync() => Task.FromResult("your-secret");
    public Task<string?> GetSessionTokenAsync() => Task.FromResult<string?>(null);
}

// Using dependency injection
var authManager = new AwsAuthManager(new CustomCredentialProvider());
```

## Environment Variables

- `AWS_ACCESS_KEY_ID` - AWS access key
- `AWS_SECRET_ACCESS_KEY` - AWS secret key
- `AWS_SESSION_TOKEN` - Optional session token for temporary credentials
- `AWS_REGION` - Primary AWS region setting
- `AWS_DEFAULT_REGION` - Fallback AWS region setting
- `AWS_ROLE_ARN` - Optional IAM role ARN to assume

## License

This project is licensed under the MIT License - see the LICENSE file for details.