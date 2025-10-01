namespace Avro.Os.Abstractions;

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