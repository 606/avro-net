namespace Avro.Os.Abstractions;

/// <summary>
/// Provides functionality to detect the current operating system.
/// </summary>
public interface IOperatingSystemDetector
{
    /// <summary>
    /// Gets the current operating system type.
    /// </summary>
    /// <returns>The operating system type.</returns>
    OperatingSystemType GetOperatingSystemType();
    
    /// <summary>
    /// Gets the current operating system as a string.
    /// </summary>
    /// <returns>The operating system name as a string.</returns>
    string GetOperatingSystemString();
    
    /// <summary>
    /// Checks if the current operating system is macOS.
    /// </summary>
    /// <returns>True if running on macOS, false otherwise.</returns>
    bool IsMacOS();
    
    /// <summary>
    /// Checks if the current operating system is Linux.
    /// </summary>
    /// <returns>True if running on Linux, false otherwise.</returns>
    bool IsLinux();
    
    /// <summary>
    /// Checks if the current operating system is Windows.
    /// </summary>
    /// <returns>True if running on Windows, false otherwise.</returns>
    bool IsWindows();
}