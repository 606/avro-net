namespace Avro.Aws.Abstractions;

/// <summary>
/// Defines methods for providing AWS region configuration.
/// </summary>
public interface IAwsRegionProvider
{
    /// <summary>
    /// Gets the current AWS region.
    /// </summary>
    /// <returns>The AWS region identifier.</returns>
    Task<string> GetRegionAsync();

    /// <summary>
    /// Sets the AWS region to use.
    /// </summary>
    /// <param name="region">The AWS region identifier.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SetRegionAsync(string region);
}