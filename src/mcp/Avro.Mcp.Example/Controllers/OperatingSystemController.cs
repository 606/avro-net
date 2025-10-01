using Avro.Os.Abstractions;
using Avro.Os.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Avro.Mcp.Example.Controllers;

[ApiController]
[Route("mcp/os")]
public class OperatingSystemController : ControllerBase
{
    private readonly IOperatingSystemDetector _osDetector;
    private readonly OperatingSystemInfo _osInfo;

    public OperatingSystemController(IOperatingSystemDetector osDetector)
    {
        _osDetector = osDetector;
        _osInfo = new OperatingSystemInfo(_osDetector);
    }

    [HttpGet("detect")]
    public IActionResult DetectOS()
    {
        var osInfo = _osInfo;
        
        return Ok(new
        {
            Type = osInfo.Type.ToString(),
            Version = osInfo.Version,
            Architecture = osInfo.Architecture
        });
    }
    
    [HttpGet("architecture")]
    public IActionResult GetArchitecture()
    {
        return Ok(new
        {
            Architecture = _osInfo.Architecture
        });
    }
    
    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new
        {
            Version = _osInfo.Version
        });
    }
    
    [HttpPost("chat/completions")]
    public IActionResult ChatCompletions([FromBody] ChatRequest request)
    {
        if (request == null)
        {
            return BadRequest("Request body cannot be empty");
        }
        string response;
        
        string prompt = string.Join("\n", request.Messages
            .Where(m => m.Role == "user")
            .SelectMany(m => m.Content.Select(c => c.Text))
            .Where(text => !string.IsNullOrEmpty(text)));
            
        if (prompt.Contains("detect", StringComparison.OrdinalIgnoreCase))
        {
            response = $"I detected that you're running {_osInfo.Type} " +
                       $"({_osInfo.Version}) on {_osInfo.Architecture} architecture.";
        }
        else if (prompt.Contains("architecture", StringComparison.OrdinalIgnoreCase))
        {
            response = $"Your system architecture is {_osInfo.Architecture}.";
        }
        else if (prompt.Contains("version", StringComparison.OrdinalIgnoreCase))
        {
            response = $"Your operating system version is {_osInfo.Version}.";
        }
        else
        {
            response = "Hello! I'm the Avro OS Detector MCP. You can ask me to:\n" +
                       "- Detect your operating system\n" +
                       "- Tell you your system architecture\n" +
                       "- Tell you your OS version";
        }
        
        return Ok(new ChatCompletionResponse
        {
            Id = Guid.NewGuid().ToString(),
            Created = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            Model = "avro-os-identifier",
            SystemFingerprint = $"avro-os-detector-{Environment.Version}",
            Choices = new List<ChatCompletionChoice>
            {
                new ChatCompletionChoice
                {
                    FinishReason = "stop",
                    Index = 0,
                    Message = new ChatMessage
                    {
                        Role = "assistant",
                        Content = new List<ChatMessageContent>
                        {
                            new ChatMessageContent
                            {
                                Text = response,
                                Type = "text"
                            }
                        }
                    }
                }
            }
        });
    }
}