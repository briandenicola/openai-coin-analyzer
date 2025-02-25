#pragma warning disable SKEXP0050

namespace ric.analyzer.api;
public static class OpenAI 
{
    private static async Task<ReadOnlyMemory<byte>> ConvertFileToMemoryStream(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.OpenReadStream().CopyToAsync(memoryStream);
        byte[] byteArray = memoryStream.ToArray();
        ReadOnlyMemory<byte> readOnlyMemory = new ReadOnlyMemory<byte>(byteArray);
        return readOnlyMemory;
    }

    public static async Task<string> AnalyzeImage(Kernel kernel, IFormFile coinImage)
    {
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


        var chatResult = await chat.GetChatMessageContentAsync(history, requestSettings, kernel);
        return chatResult.ToString();
    }
}