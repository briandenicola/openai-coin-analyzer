#pragma warning disable SKEXP0050

namespace ric.analyzer.api;
public class RicAIService
{
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
        if (coinImage == null || coinImage.Length == 0)
        {
            logger.LogError("No file uploaded.");
            throw new ArgumentException("No file uploaded.");
        }

        logger.LogInformation($"AnalyzeImage called with file: {coinImage.FileName}");

        var chat = kernel.GetRequiredService<IChatCompletionService>();
        
        var  requestSettings = new OpenAIPromptExecutionSettings(){ };

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
     
        logger.LogInformation("OpenAI API call completed.");
        logger.LogInformation($"Chat Result: {chatResult}");

        if( chatResult.InnerContent is OpenAI.Chat.ChatCompletion chatCompletion )
            logger.LogInformation($"TotalTokenCount: {chatCompletion.Usage?.TotalTokenCount ?? 0}");
        
        return chatResult.ToString();
    }
}