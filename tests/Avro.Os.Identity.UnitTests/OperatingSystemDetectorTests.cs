using Avro.Os.Abstractions;
using Avro.Os.Identity;
using System.Runtime.InteropServices;
using Xunit;

namespace Avro.Os.Identity.UnitTests;

public class OperatingSystemDetectorTests
{
    private readonly OperatingSystemDetector _detector;
    
    public OperatingSystemDetectorTests()
    {
        _detector = new OperatingSystemDetector();
    }
    
    [Fact]
    public void GetOperatingSystemType_ShouldReturnValidType()
    {
        // Act
        var result = _detector.GetOperatingSystemType();
        
        // Assert
        Assert.True(Enum.IsDefined(typeof(OperatingSystemType), result));
        Assert.NotEqual(OperatingSystemType.Unknown, result); // Should detect actual OS
    }
    
    [Fact]
    public void GetOperatingSystemString_ShouldReturnNonEmptyString()
    {
        // Act
        var result = _detector.GetOperatingSystemString();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, new[] { "macOS", "Linux", "Windows" });
    }
    
    [Fact]
    public void OSDetectionMethods_ShouldBeConsistent()
    {
        // Act
        var osType = _detector.GetOperatingSystemType();
        var isMacOS = _detector.IsMacOS();
        var isLinux = _detector.IsLinux();
        var isWindows = _detector.IsWindows();
        
        // Assert - exactly one should be true
        var trueCount = new[] { isMacOS, isLinux, isWindows }.Count(x => x);
        Assert.Equal(1, trueCount);
        
        // Assert - OS type should match the true method
        if (isMacOS) Assert.Equal(OperatingSystemType.MacOS, osType);
        if (isLinux) Assert.Equal(OperatingSystemType.Linux, osType);
        if (isWindows) Assert.Equal(OperatingSystemType.Windows, osType);
    }
    
    [Fact]
    public void GetOperatingSystemString_ShouldMatchType()
    {
        // Act
        var osType = _detector.GetOperatingSystemType();
        var osString = _detector.GetOperatingSystemString();
        
        // Assert
        var expectedString = osType switch
        {
            OperatingSystemType.MacOS => "macOS",
            OperatingSystemType.Linux => "Linux",
            OperatingSystemType.Windows => "Windows",
            _ => "Unknown"
        };
        
        Assert.Equal(expectedString, osString);
    }
    
    [Fact]
    public void IsMacOS_ShouldMatchRuntimeInformation()
    {
        // Act
        var detectorResult = _detector.IsMacOS();
        var runtimeResult = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        
        // Assert
        Assert.Equal(runtimeResult, detectorResult);
    }
    
    [Fact]
    public void IsLinux_ShouldMatchRuntimeInformation()
    {
        // Act
        var detectorResult = _detector.IsLinux();
        var runtimeResult = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        
        // Assert
        Assert.Equal(runtimeResult, detectorResult);
    }
    
    [Fact]
    public void IsWindows_ShouldMatchRuntimeInformation()
    {
        // Act
        var detectorResult = _detector.IsWindows();
        var runtimeResult = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        
        // Assert
        Assert.Equal(runtimeResult, detectorResult);
    }
}