namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddDefaultRoute(this WebApplication app)
    {
         app.MapGet( "/", () =>  $"Hello World! The time now is {DateTime.Now}" );
    }
   
    public static void AddGetResultsHandler(this WebApplication app)
    {
        app.MapGet("/result/{requestId}", (string requestId) =>
        {
            return Results.BadRequest(new { Status = "Not Implemented" });
        });  
    }
};
