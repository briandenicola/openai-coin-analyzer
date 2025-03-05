namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void CheckStatus(this WebApplication app)
    {
        app.MapGet("/status/{requestId}", (string requestId) =>
        {   
            app.Logger.LogInformation($"status Route Called with {requestId}");
            return Results.BadRequest(new { Status = "Not Implemented" });  
        });
    }
}