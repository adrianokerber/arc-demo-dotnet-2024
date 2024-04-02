using System.Diagnostics;

namespace ArcDemo2024.Hotel.Shared.OpenTelemetry;

public sealed class OtelTracingService : IDisposable
{
    public ActivitySource ActivitySource { get; }

    public OtelTracingService(TelemetrySettings? settings) => ActivitySource = new(settings!.ServiceName);

    public void Dispose()
    {
        ActivitySource.Dispose();
    }
}