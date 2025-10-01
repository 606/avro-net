namespace Avro.Os.Abstractions;

/// <summary>
/// Provides detailed information about the operating system.
/// </summary>
public interface IOperatingSystemInfo
{
    /// <summary>
    /// Gets the operating system type.
    /// </summary>
    OperatingSystemType Type { get; }
    
    /// <summary>
    /// Gets the operating system name as a string.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the operating system version.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Gets the processor architecture.
    /// </summary>
    string Architecture { get; }
    
    /// <summary>
    /// Gets a value indicating whether the operating system is 64-bit.
    /// </summary>
    bool Is64Bit { get; }
}