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

/// <summary>
/// Provides functionality to retrieve information about the current system user.
/// </summary>
public interface IUserInfoProvider
{
    /// <summary>
    /// Gets information about the current system user.
    /// </summary>
    /// <returns>User information for the current user.</returns>
    IUserInfo GetCurrentUser();
    
    /// <summary>
    /// Gets information about a specific user by username.
    /// </summary>
    /// <param name="username">The username to look up.</param>
    /// <returns>User information for the specified user, or null if not found.</returns>
    IUserInfo? GetUser(string username);
    
    /// <summary>
    /// Gets a list of all users on the system (if supported and accessible).
    /// </summary>
    /// <returns>A collection of user information objects.</returns>
    IEnumerable<IUserInfo> GetAllUsers();
    
    /// <summary>
    /// Checks if the current user has administrative privileges.
    /// </summary>
    /// <returns>True if the current user is an administrator, false otherwise.</returns>
    bool IsCurrentUserAdministrator();
}