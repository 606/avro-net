using System.Runtime.InteropServices;
using Avro.Os.Abstractions;

namespace Avro.Os.Identity;

/// <summary>
/// Provides detailed information about the operating system.
/// </summary>
public class OperatingSystemInfo : IOperatingSystemInfo
{
    private readonly IOperatingSystemDetector _detector;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OperatingSystemInfo"/> class.
    /// </summary>
    /// <param name="detector">The operating system detector.</param>
    public OperatingSystemInfo(IOperatingSystemDetector detector)
    {
        _detector = detector ?? throw new ArgumentNullException(nameof(detector));
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OperatingSystemInfo"/> class with default detector.
    /// </summary>
    public OperatingSystemInfo() : this(new OperatingSystemDetector())
    {
    }
    
    /// <summary>
    /// Gets the operating system type.
    /// </summary>
    public OperatingSystemType Type => _detector.GetOperatingSystemType();
    
    /// <summary>
    /// Gets the operating system name as a string.
    /// </summary>
    public string Name => _detector.GetOperatingSystemString();
    
    /// <summary>
    /// Gets the operating system version.
    /// </summary>
    public string Version => Environment.OSVersion.VersionString;
    
    /// <summary>
    /// Gets the processor architecture.
    /// </summary>
    public string Architecture => RuntimeInformation.ProcessArchitecture.ToString();
    
    /// <summary>
    /// Gets a value indicating whether the operating system is 64-bit.
    /// </summary>
    public bool Is64Bit => Environment.Is64BitOperatingSystem;
}