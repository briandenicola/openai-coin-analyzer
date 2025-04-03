#pragma warning disable SKEXP0050

namespace ric.analyzer.api;
public class RicAIService
{
        internal static ILogger _logger;
        internal static Kernel _kernel;
        internal static IChatCompletionService _chat;
        internal static OpenAIPromptExecutionSettings  _requestSettings ;
        internal static ChatHistory _history;

    public RicAIService(ILogger logger, Kernel kernel)
    {
        _kernel = kernel;
        _chat = _kernel.GetRequiredService<IChatCompletionService>();
        _requestSettings = new OpenAIPromptExecutionSettings(){ };
        _history = new ChatHistory();
        _history.AddSystemMessage(Constants.SYSTEM_PROMPT);
        _history.AddUserMessage(Constants.AI_PROMPT);

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

        var collectionItems= new ChatMessageContentItemCollection
        {
            new TextContent(Constants.AI_PROMPT),
            new ImageContent(await ConvertFileToMemoryStream(coinImage), coinImage.ContentType)
        };

        _history.AddUserMessage(collectionItems);
        _history.AddAssistantMessage("Please wait while I analyze the image...");

        _logger.LogInformation("Calling OpenAI API...");
        var chatResult = await _chat.GetChatMessageContentAsync(_history, _requestSettings, _kernel);
     
        _logger.LogInformation("OpenAI API call completed.");
        _logger.LogInformation($"Chat Result: {chatResult}");

        if( chatResult.InnerContent is OpenAI.Chat.ChatCompletion chatCompletion )
            _logger.LogInformation($"TotalTokenCount: {chatCompletion.Usage?.TotalTokenCount ?? 0}");
        
        _history.AddSystemMessage(chatResult.ToString());
        return chatResult.ToString();
    }
}