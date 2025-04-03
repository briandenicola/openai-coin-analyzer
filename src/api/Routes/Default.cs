namespace ric.analyzer.api;

public static partial class AppExtensions
{
    public static void AddDefaultRoute(this WebApplication app)
    {
        app.Logger.LogInformation("Default Route Called . . .");
        app.MapGet( "/", () =>  $"Hello World! The time now is {DateTime.Now}" );
    }
}