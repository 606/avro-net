namespace Avro.Mcp.Example.Controllers;

public class ChatRequest
{
    public string Model { get; set; } = string.Empty;
    public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}