namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void GetResult(this WebApplication app)
    {
        app.MapGet("/result/{requestId}", (string requestId) =>
        {
            return Results.BadRequest(new { Status = "Not Implemented" });
        });  
    }
};
