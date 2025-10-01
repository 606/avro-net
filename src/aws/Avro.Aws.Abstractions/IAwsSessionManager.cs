namespace Avro.Aws.Abstractions;

/// <summary>
/// Defines methods for managing AWS sessions.
/// </summary>
public interface IAwsSessionManager
{
    /// <summary>
    /// Starts a new AWS session with the specified duration.
    /// </summary>
    /// <param name="durationSeconds">The duration of the session in seconds.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task StartSessionAsync(int durationSeconds = 3600);

    /// <summary>
    /// Ends the current AWS session.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task EndSessionAsync();

    /// <summary>
    /// Gets a value indicating whether there is an active AWS session.
    /// </summary>
    /// <returns>True if there is an active session; otherwise, false.</returns>
    Task<bool> HasActiveSessionAsync();
}