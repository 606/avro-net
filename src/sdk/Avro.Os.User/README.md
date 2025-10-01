# Avro.Os.User

[![NuGet](https://img.shields.io/nuget/v/Avro.Os.User.svg)](https://www.nuget.org/packages/Avro.Os.User/)
[![Downloads](https://img.shields.io/nuget/dt/Avro.Os.User.svg)](https://www.nuget.org/packages/Avro.Os.User/)

A cross-platform .NET library for retrieving system user information in the Avro ecosystem. Provides comprehensive user details including username, full name, home directory, administrative privileges, and platform-specific information.

## Features

- **Cross-platform support**: Works on Windows, Linux, and macOS
- **Current user information**: Get detailed information about the currently logged-in user
- **User lookup**: Find information about specific users by username
- **Administrative privileges detection**: Check if a user has admin/root privileges
- **Platform-specific details**: Access OS-specific user information
- **Static helper methods**: Easy-to-use static API for common operations

## Installation

```bash
dotnet add package Avro.Os.User
```

## Quick Start

### Using Static Methods (Recommended for most scenarios)

```csharp
using Avro.Os.User;

// Get current user information
var currentUser = OsUser.GetCurrentUser();
Console.WriteLine($"Username: {currentUser.Username}");
Console.WriteLine($"Full Name: {currentUser.FullName}");
Console.WriteLine($"Home Directory: {currentUser.HomeDirectory}");
Console.WriteLine($"Is Administrator: {currentUser.IsAdministrator}");

// Quick access methods
var username = OsUser.GetCurrentUsername();
var homeDir = OsUser.GetCurrentUserHomeDirectory();
var isAdmin = OsUser.IsCurrentUserAdministrator();

// Look up a specific user
var user = OsUser.GetUser("john.doe");
if (user != null)
{
    Console.WriteLine($"Found user: {user}");
}
```

### Using Dependency Injection

```csharp
using Avro.Os.Abstractions;
using Avro.Os.User;
using Microsoft.Extensions.DependencyInjection;

// Register services
services.AddSingleton<IUserInfoProvider, UserInfoProvider>();

// Use in your service
public class MyService
{
    private readonly IUserInfoProvider _userInfoProvider;
    
    public MyService(IUserInfoProvider userInfoProvider)
    {
        _userInfoProvider = userInfoProvider;
    }
    
    public void DoSomething()
    {
        var currentUser = _userInfoProvider.GetCurrentUser();
        // Use user information...
    }
}
```

## API Reference

### IUserInfo Interface

```csharp
public interface IUserInfo
{
    string Username { get; }           // Username of the user
    string? FullName { get; }          // Full name (if available)
    string? HomeDirectory { get; }     // Home directory path
    string? Domain { get; }            // Domain name (Windows only)
    bool IsAdministrator { get; }      // Administrative privileges
    string? UserId { get; }            // User ID (UID/SID)
    string? GroupId { get; }           // Primary group ID (Unix only)
    string? Shell { get; }             // Shell path (Unix only)
}
```

### IUserInfoProvider Interface

```csharp
public interface IUserInfoProvider
{
    IUserInfo GetCurrentUser();                    // Get current user info
    IUserInfo? GetUser(string username);          // Get user by username
    IEnumerable<IUserInfo> GetAllUsers();         // Get all system users
    bool IsCurrentUserAdministrator();            // Check admin privileges
}
```

### Static Helper Class

```csharp
public static class OsUser
{
    static IUserInfo GetCurrentUser();                    // Get current user
    static IUserInfo? GetUser(string username);          // Get user by name
    static IEnumerable<IUserInfo> GetAllUsers();         // Get all users
    static bool IsCurrentUserAdministrator();            // Check admin status
    static string GetCurrentUsername();                  // Get current username
    static string? GetCurrentUserHomeDirectory();       // Get home directory
    static string? GetCurrentUserFullName();            // Get full name
}
```

## Platform-Specific Behavior

### Windows
- Uses Windows Identity API for user information
- Provides domain information
- Detects Administrator role membership
- Returns Windows SID as UserId

### Linux/macOS
- Uses system commands (`id`, `getent`) for user information
- Provides UID, GID, and shell information
- Detects root privileges (UID = 0)
- Reads from `/etc/passwd` for user details

### Fallback
- Uses `Environment.UserName` and `Environment.SpecialFolder.UserProfile`
- Limited information available on unsupported platforms

## Error Handling

The library is designed to be robust and handles errors gracefully:

- Returns fallback information when platform-specific calls fail
- Returns `null` for optional information that cannot be retrieved
- Never throws exceptions for normal operation failures
- Uses safe defaults when system information is unavailable

## Dependencies

- `Avro.Os.Abstractions`: Core abstractions and interfaces
- `Avro.Os.Identity`: Operating system detection functionality

## License

This project is licensed under the MIT License - see the [LICENSE](../../../LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
