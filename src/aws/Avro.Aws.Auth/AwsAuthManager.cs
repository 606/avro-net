using Avro.Aws.Abstractions;

namespace Avro.Aws.Auth;

/// <summary>
/// Provides AWS authentication and credential management capabilities.
/// </summary>
public class AwsAuthManager : IAwsCredentialProvider, IAwsRegionProvider
{
    private const string AwsAccessKeyIdEnvVar = "AWS_ACCESS_KEY_ID";
    private const string AwsSecretAccessKeyEnvVar = "AWS_SECRET_ACCESS_KEY";
    private const string AwsSessionTokenEnvVar = "AWS_SESSION_TOKEN";
    private const string AwsRegionEnvVar = "AWS_REGION";
    private const string AwsDefaultRegionEnvVar = "AWS_DEFAULT_REGION";
    private const string AwsRoleArnEnvVar = "AWS_ROLE_ARN";

    private readonly IAwsCredentialProvider? _credentialProvider;
    private readonly IAwsRegionProvider? _regionProvider;
    private string? _currentRegion;
    private string? _currentRoleArn;

    /// <summary>
    /// Initializes a new instance of the <see cref="AwsAuthManager"/> class.
    /// </summary>
    /// <param name="credentialProvider">Optional credential provider to use instead of environment variables.</param>
    /// <param name="regionProvider">Optional region provider to use instead of environment variables.</param>
    public AwsAuthManager(IAwsCredentialProvider? credentialProvider = null, IAwsRegionProvider? regionProvider = null)
    {
        _credentialProvider = credentialProvider;
        _regionProvider = regionProvider;
    }

    /// <summary>
    /// Gets the current AWS role ARN if set.
    /// </summary>
    /// <returns>The AWS role ARN, or null if not set.</returns>
    public string? GetRoleArn()
    {
        return _currentRoleArn ?? Environment.GetEnvironmentVariable(AwsRoleArnEnvVar);
    }

    /// <summary>
    /// Sets the AWS role ARN to use.
    /// </summary>
    /// <param name="roleArn">The AWS role ARN.</param>
    public void SetRoleArn(string roleArn)
    {
        if (string.IsNullOrWhiteSpace(roleArn))
        {
            throw new ArgumentException("Role ARN cannot be null or empty.", nameof(roleArn));
        }

        _currentRoleArn = roleArn;
    }

    /// <inheritdoc/>
    public async Task<string> GetRegionAsync()
    {
        if (_regionProvider != null)
        {
            return await _regionProvider.GetRegionAsync().ConfigureAwait(false);
        }

        var region = _currentRegion ??
            Environment.GetEnvironmentVariable(AwsRegionEnvVar) ??
            Environment.GetEnvironmentVariable(AwsDefaultRegionEnvVar);

        if (string.IsNullOrEmpty(region))
        {
            throw new InvalidOperationException($"AWS region not found in environment variables {AwsRegionEnvVar} or {AwsDefaultRegionEnvVar}");
        }

        return region;
    }

    /// <inheritdoc/>
    public async Task SetRegionAsync(string region)
    {
        if (_regionProvider != null)
        {
            await _regionProvider.SetRegionAsync(region).ConfigureAwait(false);
            return;
        }

        if (string.IsNullOrWhiteSpace(region))
        {
            throw new ArgumentException("Region cannot be null or empty.", nameof(region));
        }

        _currentRegion = region;
    }

    /// <inheritdoc/>
    public async Task<string> GetAccessKeyIdAsync()
    {
        if (_credentialProvider != null)
        {
            return await _credentialProvider.GetAccessKeyIdAsync().ConfigureAwait(false);
        }

        var accessKeyId = Environment.GetEnvironmentVariable(AwsAccessKeyIdEnvVar);
        if (string.IsNullOrEmpty(accessKeyId))
        {
            throw new InvalidOperationException($"AWS access key ID not found in environment variable {AwsAccessKeyIdEnvVar}");
        }

        return accessKeyId;
    }

    /// <inheritdoc/>
    public async Task<string> GetSecretAccessKeyAsync()
    {
        if (_credentialProvider != null)
        {
            return await _credentialProvider.GetSecretAccessKeyAsync().ConfigureAwait(false);
        }

        var secretKey = Environment.GetEnvironmentVariable(AwsSecretAccessKeyEnvVar);
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException($"AWS secret access key not found in environment variable {AwsSecretAccessKeyEnvVar}");
        }

        return secretKey;
    }

    /// <inheritdoc/>
    public async Task<string?> GetSessionTokenAsync()
    {
        if (_credentialProvider != null)
        {
            return await _credentialProvider.GetSessionTokenAsync().ConfigureAwait(false);
        }

        return Environment.GetEnvironmentVariable(AwsSessionTokenEnvVar);
    }
}