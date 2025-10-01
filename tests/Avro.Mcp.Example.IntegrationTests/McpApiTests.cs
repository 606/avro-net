using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using System.Threading.Tasks;

namespace Avro.Mcp.Example.IntegrationTests;

public class ApiTests : IClassFixture<WebApplicationFactory<Avro.Mcp.Example.Program>>
{
    private readonly WebApplicationFactory<Avro.Mcp.Example.Program> _factory;

    public ApiTests(WebApplicationFactory<Avro.Mcp.Example.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task DetectOS_ReturnsSuccessAndOsInfo()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/mcp/os/detect");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
        Assert.Contains("Type", content);
        Assert.Contains("Version", content);
        Assert.Contains("Architecture", content);
    }

    [Fact]
    public async Task GetArchitecture_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/mcp/os/architecture");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
        Assert.Contains("Architecture", content);
    }

    [Fact]
    public async Task Post_ChatCompletion_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new
        {
            model = "avro-os-identifier",
            messages = new[]
            {
                new
                {
                    role = "user",
                    content = new[]
                    {
                        new { type = "text", text = "Detect my operating system" }
                    }
                }
            }
        };

        // Act
        var response = await client.PostAsJsonAsync("/mcp/os/chat/completions", request);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
        Assert.Contains("choices", content);
        Assert.Contains("message", content);
    }
}