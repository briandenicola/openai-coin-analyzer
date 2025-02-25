using ric.analyser.api;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddOpenTelemetry(logging =>
{
    logging.IncludeScopes = true;
    logging.AddConsoleExporter();
    logging.AddOtlpExporter(otlpOptions =>
    {
        otlpOptions.Endpoint = new Uri(Constants.OTEL_ENDPOINT);
    });
});

builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(9091, o => o.Protocols = HttpProtocols.Http1);
    opts.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1AndHttp2);
});

builder.AddSemanticKernelConfiguration();

builder.AddCustomOtelConfiguration(
    Constants.APP_NAME,
    Constants.OTEL_ENDPOINT
);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("*");
    });
});

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();

var app = builder.Build();
app.MapPrometheusScrapingEndpoint().RequireHost("*:9091");
app.MapHealthChecks("/healthz");
app.MapControllers();

app.Logger.LogInformation($"{builder.Environment.ApplicationName} - App Run");
app.Run();
