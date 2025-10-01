using Avro.Os.Abstractions;
using Avro.Os.Identity;
using Moq;
using System.Runtime.InteropServices;
using Xunit;

namespace Avro.Os.Identity.UnitTests;

public class OperatingSystemInfoTests
{
    [Fact]
    public void Constructor_WithDetector_ShouldNotThrow()
    {
        // Arrange
        var detector = new OperatingSystemDetector();
        
        // Act & Assert
        var exception = Record.Exception(() => new OperatingSystemInfo(detector));
        Assert.Null(exception);
    }
    
    [Fact]
    public void Constructor_WithNullDetector_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new OperatingSystemInfo(null!));
    }
    
    [Fact]
    public void DefaultConstructor_ShouldNotThrow()
    {
        // Act & Assert
        var exception = Record.Exception(() => new OperatingSystemInfo());
        Assert.Null(exception);
    }
    
    [Fact]
    public void Type_ShouldReturnValidOperatingSystemType()
    {
        // Arrange
        var osInfo = new OperatingSystemInfo();
        
        // Act
        var result = osInfo.Type;
        
        // Assert
        Assert.True(Enum.IsDefined(typeof(OperatingSystemType), result));
        Assert.NotEqual(OperatingSystemType.Unknown, result);
    }
    
    [Fact]
    public void Name_ShouldReturnNonEmptyString()
    {
        // Arrange
        var osInfo = new OperatingSystemInfo();
        
        // Act
        var result = osInfo.Name;
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, new[] { "macOS", "Linux", "Windows" });
    }
    
    [Fact]
    public void Version_ShouldReturnNonEmptyString()
    {
        // Arrange
        var osInfo = new OperatingSystemInfo();
        
        // Act
        var result = osInfo.Version;
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public void Architecture_ShouldReturnValidArchitecture()
    {
        // Arrange
        var osInfo = new OperatingSystemInfo();
        
        // Act
        var result = osInfo.Architecture;
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        // Common architectures: X64, X86, Arm64, Arm
        Assert.True(result.Length > 0);
    }
    
    [Fact]
    public void Is64Bit_ShouldMatchEnvironment()
    {
        // Arrange
        var osInfo = new OperatingSystemInfo();
        
        // Act
        var result = osInfo.Is64Bit;
        var expected = Environment.Is64BitOperatingSystem;
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void Properties_ShouldBeConsistentWithDetector()
    {
        // Arrange
        var detector = new OperatingSystemDetector();
        var osInfo = new OperatingSystemInfo(detector);
        
        // Act & Assert
        Assert.Equal(detector.GetOperatingSystemType(), osInfo.Type);
        Assert.Equal(detector.GetOperatingSystemString(), osInfo.Name);
    }
    
    [Fact]
    public void WithMockedDetector_ShouldReturnMockedValues()
    {
        // Arrange
        var mockDetector = new Mock<IOperatingSystemDetector>();
        mockDetector.Setup(x => x.GetOperatingSystemType()).Returns(OperatingSystemType.Linux);
        mockDetector.Setup(x => x.GetOperatingSystemString()).Returns("TestOS");
        
        var osInfo = new OperatingSystemInfo(mockDetector.Object);
        
        // Act
        var type = osInfo.Type;
        var name = osInfo.Name;
        
        // Assert
        Assert.Equal(OperatingSystemType.Linux, type);
        Assert.Equal("TestOS", name);
        mockDetector.Verify(x => x.GetOperatingSystemType(), Times.Once);
        mockDetector.Verify(x => x.GetOperatingSystemString(), Times.Once);
    }
}