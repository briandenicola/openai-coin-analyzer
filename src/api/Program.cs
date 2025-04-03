var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(opts =>
{
    opts.ListenAnyIP(9090, o => o.Protocols = HttpProtocols.Http1);
    opts.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1);
});

builder.AddSemanticKernelConfiguration();

builder.AddCustomOtelConfiguration(
    Constants.APP_NAME,
    Constants.OTEL_ENDPOINT,
    InstrumentationSource.ActivitySourceName
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
builder.Services.AddSingleton<InstrumentationSource>();
builder.Services.AddSingleton<RicAIService>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();
var instrumentationSource = app.Services.GetRequiredService<InstrumentationSource>();

app.MapPrometheusScrapingEndpoint().RequireHost("*:9090");
app.MapHealthChecks("/healthz");
app.AddDefaultRoute();
app.AddUploadImageHandler();
app.AddGetResultsHandler();
app.UseSwagger();

logger.LogInformation($"{Constants.APP_NAME} ({instrumentationSource.Version}) - Started...");
app.Run();
