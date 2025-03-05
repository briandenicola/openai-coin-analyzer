namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddDefaultRoute(this WebApplication app)
    {
        app.Logger.LogInformation("Default Route Called");
        app.MapGet( "/", () =>  $"Hello World! The time now is {DateTime.Now}" );
    }
   
    public static void AddGetResultsHandler(this WebApplication app)
    {
        app.MapGet("/result/{requestId}", (string requestId) =>
        {
            app.Logger.LogInformation($"result Route Called with {requestId}");
            return Results.BadRequest(new { Status = "Not Implemented" });
        });  
    }
};
