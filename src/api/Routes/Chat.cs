namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void ChatHandler(this WebApplication app)
    {
        app.MapPost("/chat/{requestId}", async (string requestId, UserPrompt userPrompt, ILogger<Program> logger, RicAIService ricAIService, InstrumentationSource instrumentationSource) =>
        {   
            logger.LogInformation($"[{requestId}] - Chat Route Called . . .");

            ActivitySource activitySource = instrumentationSource.ActivitySource;
            Counter<long> countAnalyzeRequests = instrumentationSource.AnalyzeRequestCounter;

            using var activity = activitySource.StartActivity("ChatActivity");
            
            var result = await ricAIService.Chat(userPrompt.Prompt);

            return Results.Ok(new { RequestId = requestId, UserPrompt = userPrompt, Result = result });
        });

    }
}

