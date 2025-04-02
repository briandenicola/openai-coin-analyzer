namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddUploadImageHandler(this WebApplication app)
    {
        app.MapPost("/analyze", async (HttpRequest request) =>
        {
            var instrumentationSource = app.Services.GetRequiredService<InstrumentationSource>();
            ActivitySource activitySource = instrumentationSource.ActivitySource;
            Counter<long> countAnalyzeRequests = instrumentationSource.AnalyzeRequestCounter;

            using var activity = activitySource.StartActivity("AnalyzeActivity");
            countAnalyzeRequests.Add(1);

            app.Logger.LogInformation("Analyze Route Called");
            if (!request.HasFormContentType || !request.Form.Files.Any())
            {
                return Results.BadRequest("No file uploaded.");
            }
            var file = request.Form.Files[0];
            var requestId = Guid.NewGuid().ToString();
            var result = await RicAIService.AnalyzeImage( app.Services.GetRequiredService<Kernel>(), file, app.Logger);
            return Results.Ok(new { RequestId = requestId, File = file.FileName, Result = result });
        });
    }
}

