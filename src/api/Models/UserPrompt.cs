namespace ric.analyzer.api;
public record UserPrompt
{
    public string Prompt { get; set; } = string.Empty;
    public string RequestId { get; set; } = string.Empty;
    public double Temperature { get; set; } = 0.7;
}