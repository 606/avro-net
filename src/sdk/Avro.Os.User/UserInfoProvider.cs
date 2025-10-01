using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Avro.Os.Abstractions;
using Avro.Os.Identity;

namespace Avro.Os.User;

/// <summary>
/// Provides cross-platform functionality to retrieve system user information.
/// </summary>
public class UserInfoProvider : IUserInfoProvider
{
    private readonly IOperatingSystemDetector _osDetector;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserInfoProvider"/> class.
    /// </summary>
    /// <param name="osDetector">The operating system detector.</param>
    public UserInfoProvider(IOperatingSystemDetector? osDetector = null)
    {
        _osDetector = osDetector ?? new OperatingSystemDetector();
    }

    /// <inheritdoc />
    public IUserInfo GetCurrentUser()
    {
        return _osDetector.GetOperatingSystemType() switch
        {
            OperatingSystemType.Windows => GetWindowsCurrentUser(),
            OperatingSystemType.Linux => GetUnixCurrentUser(),
            OperatingSystemType.MacOS => GetUnixCurrentUser(),
            _ => GetFallbackCurrentUser()
        };
    }

    /// <inheritdoc />
    public IUserInfo? GetUser(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        return _osDetector.GetOperatingSystemType() switch
        {
            OperatingSystemType.Windows => GetWindowsUser(username),
            OperatingSystemType.Linux => GetUnixUser(username),
            OperatingSystemType.MacOS => GetUnixUser(username),
            _ => null
        };
    }

    /// <inheritdoc />
    public IEnumerable<IUserInfo> GetAllUsers()
    {
        return _osDetector.GetOperatingSystemType() switch
        {
            OperatingSystemType.Windows => GetAllWindowsUsers(),
            OperatingSystemType.Linux => GetAllUnixUsers(),
            OperatingSystemType.MacOS => GetAllUnixUsers(),
            _ => []
        };
    }

    /// <inheritdoc />
    public bool IsCurrentUserAdministrator()
    {
        return _osDetector.GetOperatingSystemType() switch
        {
            OperatingSystemType.Windows => IsWindowsUserAdministrator(),
            OperatingSystemType.Linux => IsUnixUserAdministrator(),
            OperatingSystemType.MacOS => IsUnixUserAdministrator(),
            _ => false
        };
    }

    private IUserInfo GetWindowsCurrentUser()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return GetFallbackCurrentUser();
            
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var username = Environment.UserName;
            var domain = Environment.UserDomainName;
            var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var isAdmin = IsWindowsUserAdministrator();
            var userId = identity.User?.Value;

            return new UserInfo(
                username: username,
                fullName: GetWindowsFullName(username),
                homeDirectory: homeDirectory,
                domain: domain,
                isAdministrator: isAdmin,
                userId: userId
            );
        }
        catch
        {
            return GetFallbackCurrentUser();
        }
    }

    private IUserInfo GetUnixCurrentUser()
    {
        try
        {
            var username = Environment.UserName;
            var homeDirectory = Environment.GetEnvironmentVariable("HOME");
            var shell = Environment.GetEnvironmentVariable("SHELL");
            var isAdmin = IsUnixUserAdministrator();
            var userId = ExecuteCommand("id", "-u")?.Trim();
            var groupId = ExecuteCommand("id", "-g")?.Trim();
            var fullName = GetUnixFullName(username);

            return new UserInfo(
                username: username,
                fullName: fullName,
                homeDirectory: homeDirectory,
                isAdministrator: isAdmin,
                userId: userId,
                groupId: groupId,
                shell: shell
            );
        }
        catch
        {
            return GetFallbackCurrentUser();
        }
    }

    private IUserInfo GetFallbackCurrentUser()
    {
        return new UserInfo(
            username: Environment.UserName,
            homeDirectory: Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        );
    }

    private IUserInfo? GetWindowsUser(string username)
    {
        // Windows user lookup implementation would require WMI or similar
        // For now, return null as this requires more complex implementation
        return null;
    }

    private IUserInfo? GetUnixUser(string username)
    {
        try
        {
            var userInfo = ExecuteCommand("getent", $"passwd {username}");
            if (string.IsNullOrEmpty(userInfo))
                return null;

            var parts = userInfo.Split(':');
            if (parts.Length < 6)
                return null;

            return new UserInfo(
                username: parts[0],
                fullName: parts[4],
                homeDirectory: parts[5],
                userId: parts[2],
                groupId: parts[3],
                shell: parts.Length > 6 ? parts[6] : null
            );
        }
        catch
        {
            return null;
        }
    }

    private IEnumerable<IUserInfo> GetAllWindowsUsers()
    {
        // Windows implementation would require WMI queries
        // This is a complex implementation that would need additional dependencies
        return [];
    }

    private IEnumerable<IUserInfo> GetAllUnixUsers()
    {
        try
        {
            var users = new List<IUserInfo>();
            var passwdContent = ExecuteCommand("getent", "passwd");
            
            if (string.IsNullOrEmpty(passwdContent))
                return users;

            var lines = passwdContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length >= 6)
                {
                    users.Add(new UserInfo(
                        username: parts[0],
                        fullName: parts[4],
                        homeDirectory: parts[5],
                        userId: parts[2],
                        groupId: parts[3],
                        shell: parts.Length > 6 ? parts[6] : null
                    ));
                }
            }

            return users;
        }
        catch
        {
            return [];
        }
    }

    private bool IsWindowsUserAdministrator()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return false;
            
        try
        {
            using var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch
        {
            return false;
        }
    }

    private bool IsUnixUserAdministrator()
    {
        try
        {
            var uid = ExecuteCommand("id", "-u")?.Trim();
            return uid == "0"; // Root user has UID 0
        }
        catch
        {
            return false;
        }
    }

    private string? GetWindowsFullName(string username)
    {
        try
        {
            // This would require WMI queries or Active Directory lookups
            // For now, return null
            return null;
        }
        catch
        {
            return null;
        }
    }

    private string? GetUnixFullName(string username)
    {
        try
        {
            var userInfo = ExecuteCommand("getent", $"passwd {username}");
            if (string.IsNullOrEmpty(userInfo))
                return null;

            var parts = userInfo.Split(':');
            return parts.Length > 4 ? parts[4] : null;
        }
        catch
        {
            return null;
        }
    }

    private string? ExecuteCommand(string command, string arguments)
    {
        try
        {
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = command,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return process.ExitCode == 0 ? output : null;
        }
        catch
        {
            return null;
        }
    }
}