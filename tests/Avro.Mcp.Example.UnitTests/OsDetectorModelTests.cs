using Avro.Mcp.Example.Controllers;
using Avro.Os.Identity;
using Avro.Os.Abstractions;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avro.Mcp.Example.Tests;

public class OperatingSystemControllerTests
{
    [Fact]
    public void DetectOS_ReturnsOsInfo()
    {
        // Arrange
        var osDetector = Substitute.For<IOperatingSystemDetector>();
        osDetector.GetOperatingSystemType().Returns(OperatingSystemType.Linux);
        
        var controller = new OperatingSystemController(osDetector);
        
        // Act
        var result = controller.DetectOS() as OkObjectResult;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
        
        if (result?.Value is object value)
        {
            dynamic resultValue = value;
            Assert.Equal("Linux", resultValue.Type.ToString());
        }
    }
    
    [Fact]
    public void GetArchitecture_ReturnsArchitecture()
    {
        // Arrange
        var osDetector = Substitute.For<IOperatingSystemDetector>();
        
        var controller = new OperatingSystemController(osDetector);
        
        // Act
        var result = controller.GetArchitecture() as OkObjectResult;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }
    
    [Fact]
    public void ChatCompletions_WithDetectQuery_ReturnsOsInfo()
    {
        // Arrange
        var osDetector = Substitute.For<IOperatingSystemDetector>();
        osDetector.GetOperatingSystemType().Returns(OperatingSystemType.Linux);
        
        var controller = new OperatingSystemController(osDetector);
        
        var request = new Controllers.ChatRequest
        {
            Model = "avro-os-identifier",
            Messages = new List<Controllers.ChatMessage>
            {
                new Controllers.ChatMessage
                {
                    Role = "user",
                    Content = new List<Controllers.ChatMessageContent>
                    {
                        new Controllers.ChatMessageContent { Type = "text", Text = "Detect my operating system" }
                    }
                }
            }
        };
        
        // Act
        var result = controller.ChatCompletions(request) as OkObjectResult;
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
        
        if (result?.Value is Controllers.ChatCompletionResponse response)
        {
            Assert.NotNull(response.Choices);
            Assert.Single(response.Choices);
            
            var responseText = response.Choices[0].Message.Content[0].Text;
            Assert.Contains("Linux", responseText);
        }
    }
}