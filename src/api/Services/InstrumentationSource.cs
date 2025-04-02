namespace ric.analyzer.api;

using System.Diagnostics;
using System.Diagnostics.Metrics;

public sealed class InstrumentationSource : IDisposable
{
    internal const string ActivitySourceName = "RIC.Api";
    internal const string MeterName = "Roman Imperial Coin Analyzer";
    private readonly Meter meter;

    public InstrumentationSource()
    {
        ActivitySource = new ActivitySource(ActivitySourceName, Version);
        meter = new Meter(MeterName, Version);
        AnalyzeRequestCounter = meter.CreateCounter<long>("analyze.count", description: "Counts the number of requests to analyze images");
    }

    public ActivitySource ActivitySource { get; }

    public Counter<long> AnalyzeRequestCounter { get; }

    public string Version { get; } = typeof(InstrumentationSource).Assembly.GetName().Version?.ToString() ?? "0.0.0";

    public void Dispose()
    {
        ActivitySource.Dispose();
        meter.Dispose();
    }
}