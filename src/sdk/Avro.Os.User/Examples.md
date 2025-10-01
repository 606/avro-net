# Avro.Os.User Examples

This document provides practical examples of how to use the Avro.Os.User package.

## Basic Usage Examples

### Getting Current User Information

```csharp
using Avro.Os.User;

// Get current user information using static helper
var currentUser = OsUser.GetCurrentUser();

Console.WriteLine($"Username: {currentUser.Username}");
Console.WriteLine($"Full Name: {currentUser.FullName ?? "Not available"}");
Console.WriteLine($"Home Directory: {currentUser.HomeDirectory ?? "Not available"}");
Console.WriteLine($"Is Administrator: {currentUser.IsAdministrator}");

// Platform-specific information
Console.WriteLine($"Domain: {currentUser.Domain ?? "N/A"}"); // Windows only
Console.WriteLine($"User ID: {currentUser.UserId ?? "N/A"}"); // Unix: UID, Windows: SID
Console.WriteLine($"Group ID: {currentUser.GroupId ?? "N/A"}"); // Unix only
Console.WriteLine($"Shell: {currentUser.Shell ?? "N/A"}"); // Unix only
```

### Quick Access Methods

```csharp
using Avro.Os.User;

// Quick access to common information
string username = OsUser.GetCurrentUsername();
string? homeDir = OsUser.GetCurrentUserHomeDirectory();
string? fullName = OsUser.GetCurrentUserFullName();
bool isAdmin = OsUser.IsCurrentUserAdministrator();

Console.WriteLine($"Hello {fullName ?? username}!");
Console.WriteLine($"Admin privileges: {(isAdmin ? "Yes" : "No")}");
```

### Dependency Injection Usage

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Avro.Os.Abstractions;
using Avro.Os.User;

// Program.cs or Startup.cs
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IUserInfoProvider, UserInfoProvider>();
var host = builder.Build();

// Usage in a service
public class MyService
{
    private readonly IUserInfoProvider _userInfoProvider;
    
    public MyService(IUserInfoProvider userInfoProvider)
    {
        _userInfoProvider = userInfoProvider;
    }
    
    public void LogUserInfo()
    {
        var user = _userInfoProvider.GetCurrentUser();
        Console.WriteLine($"Service running as: {user.Username}");
        
        if (user.IsAdministrator)
        {
            Console.WriteLine("⚠️  Running with administrative privileges");
        }
    }
}
```

### User Lookup

```csharp
using Avro.Os.User;

// Look up a specific user (platform-dependent support)
var user = OsUser.GetUser("john.doe");
if (user != null)
{
    Console.WriteLine($"Found user: {user}");
    Console.WriteLine($"Home: {user.HomeDirectory}");
}
else
{
    Console.WriteLine("User not found or lookup not supported on this platform");
}
```

### List All Users (Unix-like systems)

```csharp
using Avro.Os.User;

// Get all users (primarily works on Unix-like systems)
var allUsers = OsUser.GetAllUsers();

Console.WriteLine($"Found {allUsers.Count()} users:");
foreach (var user in allUsers.Take(10)) // Show first 10
{
    Console.WriteLine($"  {user.Username} (UID: {user.UserId})");
}
```

## Platform-Specific Examples

### Windows-Specific Information

```csharp
using Avro.Os.User;
using Avro.Os.Identity;

if (OsIdentity.IsWindows())
{
    var user = OsUser.GetCurrentUser();
    Console.WriteLine($"Windows User: {user.Domain}\\{user.Username}");
    Console.WriteLine($"SID: {user.UserId}");
    
    if (user.IsAdministrator)
    {
        Console.WriteLine("User is a member of the Administrators group");
    }
}
```

### Unix/Linux-Specific Information

```csharp
using Avro.Os.User;
using Avro.Os.Identity;

if (OsIdentity.IsLinux() || OsIdentity.IsMacOS())
{
    var user = OsUser.GetCurrentUser();
    Console.WriteLine($"Unix User: {user.Username}");
    Console.WriteLine($"UID: {user.UserId}");
    Console.WriteLine($"GID: {user.GroupId}");
    Console.WriteLine($"Shell: {user.Shell}");
    Console.WriteLine($"Home: {user.HomeDirectory}");
    
    if (user.UserId == "0")
    {
        Console.WriteLine("Running as root");
    }
}
```

## Error Handling

```csharp
using Avro.Os.User;

try
{
    var user = OsUser.GetCurrentUser();
    Console.WriteLine($"Current user: {user.Username}");
}
catch (Exception ex)
{
    // The library is designed to be robust and rarely throws exceptions
    // This catch block is here for completeness
    Console.WriteLine($"Error getting user info: {ex.Message}");
}

// The library prefers returning null or default values instead of throwing
var specificUser = OsUser.GetUser("nonexistent");
if (specificUser == null)
{
    Console.WriteLine("User not found or lookup not supported");
}
```

## Advanced Usage

### Custom User Info Provider

```csharp
using Avro.Os.Abstractions;
using Avro.Os.User;
using Avro.Os.Identity;

// Create a custom provider with specific OS detector
var osDetector = new OperatingSystemDetector();
var userProvider = new UserInfoProvider(osDetector);

var currentUser = userProvider.GetCurrentUser();
Console.WriteLine($"OS: {osDetector.GetOperatingSystemString()}");
Console.WriteLine($"User: {currentUser.Username}");
```

### Conditional Logic Based on User Privileges

```csharp
using Avro.Os.User;

public class ApplicationBootstrapper
{
    public void Initialize()
    {
        if (OsUser.IsCurrentUserAdministrator())
        {
            Console.WriteLine("🔐 Administrative mode enabled");
            EnableAdvancedFeatures();
        }
        else
        {
            Console.WriteLine("👤 Running in standard user mode");
            EnableBasicFeatures();
        }
    }
    
    private void EnableAdvancedFeatures()
    {
        // Configure application with full privileges
    }
    
    private void EnableBasicFeatures()
    {
        // Configure application with limited privileges
    }
}
```

## Integration with Logging

```csharp
using Microsoft.Extensions.Logging;
using Avro.Os.User;

public class UserAwareService
{
    private readonly ILogger<UserAwareService> _logger;
    
    public UserAwareService(ILogger<UserAwareService> logger)
    {
        _logger = logger;
        LogUserContext();
    }
    
    private void LogUserContext()
    {
        var user = OsUser.GetCurrentUser();
        
        _logger.LogInformation("Service started by user: {Username} ({FullName})", 
            user.Username, user.FullName ?? "Unknown");
            
        if (user.IsAdministrator)
        {
            _logger.LogWarning("Service running with administrative privileges");
        }
        
        _logger.LogDebug("User details - Home: {Home}, Domain: {Domain}", 
            user.HomeDirectory, user.Domain);
    }
}
```