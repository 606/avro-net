namespace Avro.Mcp.Example.Controllers;

public class ChatCompletionResponse
{
    public string Id { get; set; } = string.Empty;
    public long Created { get; set; }
    public string Model { get; set; } = string.Empty;
    public string SystemFingerprint { get; set; } = string.Empty;
    public List<ChatCompletionChoice> Choices { get; set; } = new List<ChatCompletionChoice>();
}