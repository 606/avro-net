namespace Avro.Os.Abstractions;

/// <summary>
/// Represents information about a system user.
/// </summary>
public interface IUserInfo
{
    /// <summary>
    /// Gets the username of the current user.
    /// </summary>
    string Username { get; }
    
    /// <summary>
    /// Gets the full name of the current user (if available).
    /// </summary>
    string? FullName { get; }
    
    /// <summary>
    /// Gets the home directory path of the current user.
    /// </summary>
    string? HomeDirectory { get; }
    
    /// <summary>
    /// Gets the domain name of the current user (Windows) or empty string for other platforms.
    /// </summary>
    string? Domain { get; }
    
    /// <summary>
    /// Gets a value indicating whether the current user has administrative privileges.
    /// </summary>
    bool IsAdministrator { get; }
    
    /// <summary>
    /// Gets the user ID (UID on Unix-like systems, SID on Windows).
    /// </summary>
    string? UserId { get; }
    
    /// <summary>
    /// Gets the primary group ID (GID on Unix-like systems).
    /// </summary>
    string? GroupId { get; }
    
    /// <summary>
    /// Gets the shell path (Unix-like systems only).
    /// </summary>
    string? Shell { get; }
}