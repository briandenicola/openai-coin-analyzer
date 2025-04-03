namespace ric.analyzer.api;

public static partial class AppExtensions
{   
    public static void AddGetResultsHandler(this WebApplication app)
    {
        app.MapGet("/result/{requestId}", (string requestId) =>
        {
            app.Logger.LogInformation($"[{requestId}] - Result Route Called . . .");
            return Results.BadRequest(new { Status = "Not Implemented" });
        });  
    }
}
