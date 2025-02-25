namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void UploadImage(this WebApplication app)
    {
        app.MapPost("/upload", async (HttpRequest request) =>
        {
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

