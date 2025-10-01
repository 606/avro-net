namespace Avro.Mcp.Example.Controllers;

public class ChatRequest
{
    public string Model { get; set; } = string.Empty;
    public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}

public class ChatMessage
{
    public string Role { get; set; } = string.Empty;
    public List<ChatMessageContent> Content { get; set; } = new List<ChatMessageContent>();
}

public class ChatMessageContent
{
    public string Type { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public class ChatCompletionResponse
{
    public string Id { get; set; } = string.Empty;
    public long Created { get; set; }
    public string Model { get; set; } = string.Empty;
    public string SystemFingerprint { get; set; } = string.Empty;
    public List<ChatCompletionChoice> Choices { get; set; } = new List<ChatCompletionChoice>();
}

public class ChatCompletionChoice
{
    public string FinishReason { get; set; } = string.Empty;
    public int Index { get; set; }
    public ChatMessage Message { get; set; } = new ChatMessage();
}