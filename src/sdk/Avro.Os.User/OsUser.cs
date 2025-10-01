using Avro.Os.Abstractions;

namespace Avro.Os.User;

/// <summary>
/// Static helper class for quick access to user information functionality.
/// </summary>
public static class OsUser
{
    private static readonly IUserInfoProvider _provider = new UserInfoProvider();

    /// <summary>
    /// Gets information about the current system user.
    /// </summary>
    /// <returns>User information for the current user.</returns>
    public static IUserInfo GetCurrentUser() => _provider.GetCurrentUser();

    /// <summary>
    /// Gets information about a specific user by username.
    /// </summary>
    /// <param name="username">The username to look up.</param>
    /// <returns>User information for the specified user, or null if not found.</returns>
    public static IUserInfo? GetUser(string username) => _provider.GetUser(username);

    /// <summary>
    /// Gets a list of all users on the system (if supported and accessible).
    /// </summary>
    /// <returns>A collection of user information objects.</returns>
    public static IEnumerable<IUserInfo> GetAllUsers() => _provider.GetAllUsers();

    /// <summary>
    /// Checks if the current user has administrative privileges.
    /// </summary>
    /// <returns>True if the current user is an administrator, false otherwise.</returns>
    public static bool IsCurrentUserAdministrator() => _provider.IsCurrentUserAdministrator();

    /// <summary>
    /// Gets the username of the current user.
    /// </summary>
    /// <returns>The current username.</returns>
    public static string GetCurrentUsername() => GetCurrentUser().Username;

    /// <summary>
    /// Gets the home directory of the current user.
    /// </summary>
    /// <returns>The current user's home directory path.</returns>
    public static string? GetCurrentUserHomeDirectory() => GetCurrentUser().HomeDirectory;

    /// <summary>
    /// Gets the full name of the current user (if available).
    /// </summary>
    /// <returns>The current user's full name.</returns>
    public static string? GetCurrentUserFullName() => GetCurrentUser().FullName;
}