namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddUploadImageHandler(this WebApplication app)
    {
        app.MapPost("/analyze", async (HttpRequest request) =>
        {
            app.Logger.LogInformation("analyze Route Called");
            if (!request.HasFormContentType || !request.Form.Files.Any())
            {
                return Results.BadRequest("No file uploaded.");
            }
            var file = request.Form.Files[0];
            var requestId = Guid.NewGuid().ToString();
            var result = await OpenAI.AnalyzeImage( app.Services.GetRequiredService<Kernel>(), file);
            return Results.Ok(new { RequestId = requestId, File = file.FileName, Result = result });
        });
    }
}

