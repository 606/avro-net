namespace Avro.Mcp.Example.Controllers;

public class ChatMessage
{
    public string Role { get; set; } = string.Empty;
    public List<ChatMessageContent> Content { get; set; } = new List<ChatMessageContent>();
}