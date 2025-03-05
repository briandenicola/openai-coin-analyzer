#pragma warning disable SKEXP0050

namespace ric.analyzer.api;
public class OpenAI 
{
    private readonly ILogger _logger;

    public OpenAI(ILogger<OpenAI> logger)
    {
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

    public static async Task<string> AnalyzeImage(Kernel kernel, IFormFile coinImage, ILogger logger)
    {
        logger.LogInformation("AnalyzeImage Routine Called...");
        var chat = kernel.GetRequiredService<IChatCompletionService>();
        
        var  requestSettings = new OpenAIPromptExecutionSettings()
        {
            MaxTokens = 4096,    
        };

        var history = new ChatHistory();
        history.AddSystemMessage(Constants.SYSTEM_PROMPT);
        history.AddUserMessage(Constants.AI_PROMPT);
    
        var collectionItems= new ChatMessageContentItemCollection
        {
            new TextContent(Constants.AI_PROMPT),
            new ImageContent(await ConvertFileToMemoryStream(coinImage), coinImage.ContentType)
        };
        history.AddUserMessage(collectionItems);
        history.AddAssistantMessage("Please wait while I analyze the image...");

        logger.LogInformation("Calling OpenAI API...");
        var chatResult = await chat.GetChatMessageContentAsync(history, requestSettings, kernel);
        return chatResult.ToString();
    }
}