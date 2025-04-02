namespace ric.analyzer.api;

public static class ProgramExtensions
{
    private static DefaultAzureCredential credential = new DefaultAzureCredential();
    public static void AddSemanticKernelConfiguration( this WebApplicationBuilder builder )
    {
        var skBuilder = Kernel.CreateBuilder();
        skBuilder.AddAzureOpenAIChatCompletion(Constants.OPENAI_MODEL, Constants.OPENAI_ENDPOINT, credential);
                    
        var kernel = skBuilder.Build();
        builder.Services.AddSingleton<Kernel>(kernel);
    }

    public static void AddCustomOtelConfiguration(
        this WebApplicationBuilder builder, 
        string ApplicationName, 
        string otelConnectionString,
        string ricActivitySourceName)
    {
        AppContext.SetSwitch("Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnosticsSensitive", true);
        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService("RomanImperialCoinAnalyzer");

        builder.Logging.ClearProviders();
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddOpenTelemetry(options =>
            {
                options.SetResourceBuilder(resourceBuilder);
                options.AddConsoleExporter();
                options.AddOtlpExporter(options => options.Endpoint = new Uri(otelConnectionString));
                options.IncludeFormattedMessage = true;
                options.IncludeScopes = true;
            });
            builder.SetMinimumLevel(LogLevel.Information);
        });
        _ = builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);
        
        var otel = builder.Services.AddOpenTelemetry();
        
        otel.ConfigureResource(resource => resource
            .AddService(serviceName: ApplicationName));

        otel.WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource(ricActivitySourceName)
            .AddSource("Microsoft.SemanticKernel*")
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri(otelConnectionString);
            })
        );

        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter(ricActivitySourceName)
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddMeter("Microsoft.SemanticKernel*")
            .AddPrometheusExporter(o => o.DisableTotalNameSuffixForCounters = true) 
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri(otelConnectionString);
            })
        );

        ILogger logger = loggerFactory.CreateLogger("Program");
        logger.LogInformation("OpenTelemetry configured with Azure Monitor");
        logger.LogInformation($"Application Name: {ApplicationName}");
    }
}