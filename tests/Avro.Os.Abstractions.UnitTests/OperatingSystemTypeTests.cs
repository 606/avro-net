using Avro.Os.Abstractions;
using Xunit;

namespace Avro.Os.Abstractions.UnitTests;

public class OperatingSystemTypeTests
{
    [Fact]
    public void OperatingSystemType_ShouldHaveExpectedValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<OperatingSystemType>();
        
        // Assert
        Assert.Contains(OperatingSystemType.Unknown, values);
        Assert.Contains(OperatingSystemType.MacOS, values);
        Assert.Contains(OperatingSystemType.Linux, values);
        Assert.Contains(OperatingSystemType.Windows, values);
        Assert.Equal(4, values.Length);
    }
    
    [Theory]
    [InlineData(OperatingSystemType.Unknown, 0)]
    [InlineData(OperatingSystemType.MacOS, 1)]
    [InlineData(OperatingSystemType.Linux, 2)]
    [InlineData(OperatingSystemType.Windows, 3)]
    public void OperatingSystemType_ShouldHaveCorrectValues(OperatingSystemType osType, int expectedValue)
    {
        // Act & Assert
        Assert.Equal(expectedValue, (int)osType);
    }
    
    [Theory]
    [InlineData("Unknown", OperatingSystemType.Unknown)]
    [InlineData("MacOS", OperatingSystemType.MacOS)]
    [InlineData("Linux", OperatingSystemType.Linux)]
    [InlineData("Windows", OperatingSystemType.Windows)]
    public void OperatingSystemType_ShouldParseFromString(string input, OperatingSystemType expected)
    {
        // Act
        var result = Enum.Parse<OperatingSystemType>(input);
        
        // Assert
        Assert.Equal(expected, result);
    }
}