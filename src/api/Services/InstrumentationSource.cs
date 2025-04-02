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
        var version = typeof(InstrumentationSource).Assembly.GetName().Version?.ToString();
        ActivitySource = new ActivitySource(ActivitySourceName, version);
        meter = new Meter(MeterName, version);
        AnalyzeRequestCounter = meter.CreateCounter<long>("analyze.count", description: "Counts the number of requests to analyze images");
    }

    public ActivitySource ActivitySource { get; }

    public Counter<long> AnalyzeRequestCounter { get; }

    public void Dispose()
    {
        ActivitySource.Dispose();
        meter.Dispose();
    }
}