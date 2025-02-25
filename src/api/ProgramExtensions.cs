namespace ric.analyser.api;

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

    public static void AddCustomOtelConfiguration(this WebApplicationBuilder builder, string ApplicationName, string otelConnectionString)
    {
        AppContext.SetSwitch("Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnosticsSensitive", true);

        var otel = builder.Services.AddOpenTelemetry();

        var ricApiMeter = new Meter("Roman Imperial Coin Analyzer", "2.0.0");
        var ricActivitySource = new ActivitySource("ric.api");

        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService("RomanImperialCoinAnalyzer");

        otel.ConfigureResource(resource => resource
            .AddService(serviceName: ApplicationName));

        otel.WithMetrics(metrics => metrics
            .AddAspNetCoreInstrumentation()
            .AddRuntimeInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter(ricActivitySource.Name)
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            .AddPrometheusExporter(o => o.DisableTotalNameSuffixForCounters = true) //https://github.com/open-telemetry/opentelemetry-dotnet/issues/5502
            .AddConsoleExporter()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri(otelConnectionString);
            })
        );

        otel.WithTracing(tracing => tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddSource(ricActivitySource.Name)
            .AddConsoleExporter()
            .AddOtlpExporter(opt =>
            {
                opt.Endpoint = new Uri(otelConnectionString);
            })
        );

        using var traceProvider = Sdk.CreateTracerProviderBuilder()
            .SetResourceBuilder(resourceBuilder)
            .AddSource("Microsoft.SemanticKernel*")
            .AddOtlpExporter(options => options.Endpoint = new Uri(otelConnectionString))
            .Build();

        using var meterProvider = Sdk.CreateMeterProviderBuilder()
            .SetResourceBuilder(resourceBuilder)
            .AddMeter("Microsoft.SemanticKernel*")
            .AddConsoleExporter()
            .AddOtlpExporter(options => options.Endpoint = new Uri(otelConnectionString))
            .Build();

        using var loggerFactory = LoggerFactory.Create(builder =>
        {
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

    }
}