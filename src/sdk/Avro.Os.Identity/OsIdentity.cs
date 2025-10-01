using System.Runtime.InteropServices;
using Avro.Os.Abstractions;

namespace Avro.Os.Identity;

/// <summary>
/// Provides functionality to determine the current operating system.
/// </summary>
public class OperatingSystemDetector : IOperatingSystemDetector
{
    /// <summary>
    /// Gets the current operating system type.
    /// </summary>
    /// <returns>The operating system type.</returns>
    public OperatingSystemType GetOperatingSystemType()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return OperatingSystemType.MacOS;
        }
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return OperatingSystemType.Linux;
        }
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return OperatingSystemType.Windows;
        }
        
        return OperatingSystemType.Unknown;
    }
    
    /// <summary>
    /// Gets the current operating system as a string.
    /// </summary>
    /// <returns>The operating system name as a string.</returns>
    public string GetOperatingSystemString()
    {
        return GetOperatingSystemType() switch
        {
            OperatingSystemType.MacOS => "macOS",
            OperatingSystemType.Linux => "Linux",
            OperatingSystemType.Windows => "Windows",
            _ => "Unknown"
        };
    }
    
    /// <summary>
    /// Checks if the current operating system is macOS.
    /// </summary>
    /// <returns>True if running on macOS, false otherwise.</returns>
    public bool IsMacOS() => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
    
    /// <summary>
    /// Checks if the current operating system is Linux.
    /// </summary>
    /// <returns>True if running on Linux, false otherwise.</returns>
    public bool IsLinux() => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    
    /// <summary>
    /// Checks if the current operating system is Windows.
    /// </summary>
    /// <returns>True if running on Windows, false otherwise.</returns>
    public bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
}

/// <summary>
/// Static helper class for quick OS detection.
/// </summary>
public static class OsIdentity
{
    private static readonly IOperatingSystemDetector _detector = new OperatingSystemDetector();
    
    /// <summary>
    /// Gets the current operating system type.
    /// </summary>
    /// <returns>The operating system type.</returns>
    public static OperatingSystemType GetOperatingSystemType() => _detector.GetOperatingSystemType();
    
    /// <summary>
    /// Gets the current operating system as a string.
    /// </summary>
    /// <returns>The operating system name as a string.</returns>
    public static string GetOperatingSystemString() => _detector.GetOperatingSystemString();
    
    /// <summary>
    /// Checks if the current operating system is macOS.
    /// </summary>
    /// <returns>True if running on macOS, false otherwise.</returns>
    public static bool IsMacOS() => _detector.IsMacOS();
    
    /// <summary>
    /// Checks if the current operating system is Linux.
    /// </summary>
    /// <returns>True if running on Linux, false otherwise.</returns>
    public static bool IsLinux() => _detector.IsLinux();
    
    /// <summary>
    /// Checks if the current operating system is Windows.
    /// </summary>
    /// <returns>True if running on Windows, false otherwise.</returns>
    public static bool IsWindows() => _detector.IsWindows();
}