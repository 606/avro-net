using Avro.Os.Abstractions;
using Avro.Os.Identity;
using Xunit;

namespace Avro.Os.Identity.UnitTests;

public class OsIdentityTests
{
    [Fact]
    public void GetOperatingSystemType_ShouldReturnValidType()
    {
        // Act
        var result = OsIdentity.GetOperatingSystemType();
        
        // Assert
        Assert.True(Enum.IsDefined(typeof(OperatingSystemType), result));
        Assert.NotEqual(OperatingSystemType.Unknown, result);
    }
    
    [Fact]
    public void GetOperatingSystemString_ShouldReturnValidString()
    {
        // Act
        var result = OsIdentity.GetOperatingSystemString();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, new[] { "macOS", "Linux", "Windows" });
    }
    
    [Fact]
    public void StaticMethods_ShouldBeConsistentWithInstanceMethods()
    {
        // Arrange
        var detector = new OperatingSystemDetector();
        
        // Act & Assert
        Assert.Equal(detector.GetOperatingSystemType(), OsIdentity.GetOperatingSystemType());
        Assert.Equal(detector.GetOperatingSystemString(), OsIdentity.GetOperatingSystemString());
        Assert.Equal(detector.IsMacOS(), OsIdentity.IsMacOS());
        Assert.Equal(detector.IsLinux(), OsIdentity.IsLinux());
        Assert.Equal(detector.IsWindows(), OsIdentity.IsWindows());
    }
    
    [Fact]
    public void OSDetectionMethods_ShouldHaveExactlyOneTrue()
    {
        // Act
        var isMacOS = OsIdentity.IsMacOS();
        var isLinux = OsIdentity.IsLinux();
        var isWindows = OsIdentity.IsWindows();
        
        // Assert
        var trueCount = new[] { isMacOS, isLinux, isWindows }.Count(x => x);
        Assert.Equal(1, trueCount);
    }
    
    [Fact]
    public void GetOperatingSystemType_ShouldMatchBooleanMethods()
    {
        // Act
        var osType = OsIdentity.GetOperatingSystemType();
        var isMacOS = OsIdentity.IsMacOS();
        var isLinux = OsIdentity.IsLinux();
        var isWindows = OsIdentity.IsWindows();
        
        // Assert
        if (isMacOS) Assert.Equal(OperatingSystemType.MacOS, osType);
        if (isLinux) Assert.Equal(OperatingSystemType.Linux, osType);
        if (isWindows) Assert.Equal(OperatingSystemType.Windows, osType);
    }
}