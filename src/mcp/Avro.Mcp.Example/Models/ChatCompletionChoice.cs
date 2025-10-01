namespace Avro.Mcp.Example.Controllers;

public class ChatCompletionChoice
{
    public string FinishReason { get; set; } = string.Empty;
    public int Index { get; set; }
    public ChatMessage Message { get; set; } = new ChatMessage();
}