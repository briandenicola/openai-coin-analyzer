namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddUploadImageHandler(this WebApplication app)
    {
        app.MapPost("/analyze", async (HttpRequest request, ILogger logger, RicAIService ricAIService, InstrumentationSource instrumentationSource) =>
        {
            ActivitySource activitySource = instrumentationSource.ActivitySource;
            Counter<long> countAnalyzeRequests = instrumentationSource.AnalyzeRequestCounter;

            using var activity = activitySource.StartActivity("AnalyzeActivity");
            countAnalyzeRequests.Add(1);

            logger.LogInformation("Analyze Route Called");
            if (!request.HasFormContentType || !request.Form.Files.Any())
            {
                return Results.BadRequest("No file uploaded.");
            }
            var file = request.Form.Files[0];
            var requestId = Guid.NewGuid().ToString();
            var result = await ricAIService.AnalyzeImage(file);
            return Results.Ok(new { RequestId = requestId, File = file.FileName, Result = result });
        });
    }
}

