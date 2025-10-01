namespace Avro.Aws.Abstractions;

/// <summary>
/// Defines methods for providing AWS credentials.
/// </summary>
public interface IAwsCredentialProvider
{
    /// <summary>
    /// Gets the AWS access key ID.
    /// </summary>
    /// <returns>The AWS access key ID.</returns>
    Task<string> GetAccessKeyIdAsync();

    /// <summary>
    /// Gets the AWS secret access key.
    /// </summary>
    /// <returns>The AWS secret access key.</returns>
    Task<string> GetSecretAccessKeyAsync();

    /// <summary>
    /// Gets the AWS session token if available.
    /// </summary>
    /// <returns>The AWS session token, or null if not using temporary credentials.</returns>
    Task<string?> GetSessionTokenAsync();
}