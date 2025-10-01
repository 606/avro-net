using Avro.Os.Abstractions;
using System.Reflection;
using Xunit;

namespace Avro.Os.Abstractions.UnitTests;

public class InterfaceValidationTests
{
    [Fact]
    public void IOperatingSystemDetector_ShouldHaveCorrectMethods()
    {
        // Arrange
        var interfaceType = typeof(IOperatingSystemDetector);
        
        // Act
        var methods = interfaceType.GetMethods();
        
        // Assert
        Assert.Contains(methods, m => m.Name == "GetOperatingSystemType" && m.ReturnType == typeof(OperatingSystemType));
        Assert.Contains(methods, m => m.Name == "GetOperatingSystemString" && m.ReturnType == typeof(string));
        Assert.Contains(methods, m => m.Name == "IsMacOS" && m.ReturnType == typeof(bool));
        Assert.Contains(methods, m => m.Name == "IsLinux" && m.ReturnType == typeof(bool));
        Assert.Contains(methods, m => m.Name == "IsWindows" && m.ReturnType == typeof(bool));
    }
    
    [Fact]
    public void IOperatingSystemInfo_ShouldHaveCorrectProperties()
    {
        // Arrange
        var interfaceType = typeof(IOperatingSystemInfo);
        
        // Act
        var properties = interfaceType.GetProperties();
        
        // Assert
        Assert.Contains(properties, p => p.Name == "Type" && p.PropertyType == typeof(OperatingSystemType));
        Assert.Contains(properties, p => p.Name == "Name" && p.PropertyType == typeof(string));
        Assert.Contains(properties, p => p.Name == "Version" && p.PropertyType == typeof(string));
        Assert.Contains(properties, p => p.Name == "Architecture" && p.PropertyType == typeof(string));
        Assert.Contains(properties, p => p.Name == "Is64Bit" && p.PropertyType == typeof(bool));
        Assert.Equal(5, properties.Length);
    }
    
    [Fact]
    public void IOperatingSystemDetector_ShouldBeInterface()
    {
        // Arrange & Act
        var interfaceType = typeof(IOperatingSystemDetector);
        
        // Assert
        Assert.True(interfaceType.IsInterface);
        Assert.True(interfaceType.IsPublic);
    }
    
    [Fact]
    public void IOperatingSystemInfo_ShouldBeInterface()
    {
        // Arrange & Act
        var interfaceType = typeof(IOperatingSystemInfo);
        
        // Assert
        Assert.True(interfaceType.IsInterface);
        Assert.True(interfaceType.IsPublic);
    }
}