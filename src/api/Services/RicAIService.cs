
namespace ric.analyzer.api;
public class RicAIService
{
    internal static ILogger _logger;
    internal static AIAgent _agent;
    
    public RicAIService(ILogger<RicAIService> logger, AIAgent agent)
    {
        _agent = agent;
        _logger = logger;
    }
    
    private static async Task<ReadOnlyMemory<byte>> ConvertFileToMemoryStream(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        byte[] byteArray = memoryStream.ToArray();
        ReadOnlyMemory<byte> readOnlyMemory = new ReadOnlyMemory<byte>(byteArray);
        return readOnlyMemory;
    }

    public async Task<string> AnalyzeImage(IFormFile coinImage )
    {
        if (coinImage == null || coinImage.Length == 0)
        {
            _logger.LogError("No file uploaded.");
            throw new ArgumentException("No file uploaded.");
        }

        _logger.LogInformation($"AnalyzeImage called with file: {coinImage.FileName}");

        var data = new List<AIContent>
        {
            new TextContent(Constants.AI_PROMPT),
            new DataContent(await ConvertFileToMemoryStream(coinImage), coinImage.ContentType)
        };

        ChatMessage message = new(ChatRole.User, data );

        var response = await _agent.RunAsync(message);

        return response.ToString();
    }
}