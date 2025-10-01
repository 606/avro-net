using Avro.Os.Abstractions;

namespace Avro.Os.Identity;

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