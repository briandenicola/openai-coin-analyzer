#pragma warning disable SKEXP0001

namespace ric.analyzer.api;
public class RicAIService
{
    internal static ILogger _logger;
    internal static Kernel _kernel;
    internal static IChatCompletionService _chat;
    internal static OpenAIPromptExecutionSettings  _requestSettings ;
    internal static ChatHistory _history;
    internal static ChatHistoryTruncationReducer _reducer;

    public RicAIService(ILogger<RicAIService> logger, Kernel kernel)
    {
        _kernel = kernel;
        _logger = logger;

        _chat = _kernel.GetRequiredService<IChatCompletionService>();
        _requestSettings = new OpenAIPromptExecutionSettings(){ };
        _reducer = new ChatHistoryTruncationReducer(targetCount: 2);
        
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

    private static async Task<ChatMessageContent> CallOpenAIEndpoint()
    {
        _logger.LogInformation("OpenAI API call started . . . ");
        var chatResult = await _chat.GetChatMessageContentAsync(_history, _requestSettings, _kernel);
        _logger.LogInformation("OpenAI API call completed . . . ");

        if( chatResult.InnerContent is OpenAI.Chat.ChatCompletion chatCompletion )
            _logger.LogInformation($"TotalTokenCount: {chatCompletion.Usage?.TotalTokenCount ?? 0} . . .");
        
        _logger.LogInformation($"Analyze Result: {chatResult}");
        _logger.LogInformation($"Adding ChatResult to History . . . ");
        _history.AddSystemMessage(chatResult.ToString());

        return chatResult;
    }

    public async Task TrimHistory()
    {
        _logger.LogInformation("Trimming history . . . ");
        var reducedMessages = await _reducer.ReduceAsync(_history);

        if (reducedMessages is not null)
            _history = [.. reducedMessages];
        
        var response = CallOpenAIEndpoint();
        _history.AddSystemMessage(response.ToString());

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
        
        return (await CallOpenAIEndpoint()).ToString();
    }

    public async Task<string> Chat(string userPrompt)
    {
        if (string.IsNullOrWhiteSpace(userPrompt))
        {
            _logger.LogError("User prompt is empty or null.");
            throw new ArgumentException("User prompt cannot be empty or null.");
        }

        _logger.LogInformation($"Chat called with prompt: {userPrompt}");
        _history.AddUserMessage(userPrompt);

        var response = await CallOpenAIEndpoint();
        _history.AddSystemMessage(response.ToString());

        return response.ToString();
    }
}