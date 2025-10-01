using Avro.Os.Abstractions;

namespace Avro.Os.UserInfo;

/// <summary>
/// Represents information about a system user.
/// </summary>
public class UserInfo : IUserInfo
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInfo"/> class.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="fullName">The full name of the user.</param>
    /// <param name="homeDirectory">The home directory path.</param>
    /// <param name="domain">The domain name.</param>
    /// <param name="isAdministrator">Whether the user has administrative privileges.</param>
    /// <param name="userId">The user ID.</param>
    /// <param name="groupId">The group ID.</param>
    /// <param name="shell">The shell path.</param>
    public UserInfo(
        string username,
        string? fullName = null,
        string? homeDirectory = null,
        string? domain = null,
        bool isAdministrator = false,
        string? userId = null,
        string? groupId = null,
        string? shell = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(username);
        Username = username;
        FullName = fullName;
        HomeDirectory = homeDirectory;
        Domain = domain;
        IsAdministrator = isAdministrator;
        UserId = userId;
        GroupId = groupId;
        Shell = shell;
    }

    /// <inheritdoc />
    public string Username { get; }
    
    /// <inheritdoc />
    public string? FullName { get; }
    
    /// <inheritdoc />
    public string? HomeDirectory { get; }
    
    /// <inheritdoc />
    public string? Domain { get; }
    
    /// <inheritdoc />
    public bool IsAdministrator { get; }
    
    /// <inheritdoc />
    public string? UserId { get; }
    
    /// <inheritdoc />
    public string? GroupId { get; }
    
    /// <inheritdoc />
    public string? Shell { get; }

    /// <summary>
    /// Returns a string representation of the user information.
    /// </summary>
    /// <returns>A string containing the username and full name (if available).</returns>
    public override string ToString()
    {
        return string.IsNullOrEmpty(FullName) ? Username : $"{Username} ({FullName})";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is UserInfo other && Username.Equals(other.Username, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return Username.GetHashCode(StringComparison.OrdinalIgnoreCase);
    }
}