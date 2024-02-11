namespace Common.OpenTelemetry;

public sealed class OpenTelemetryConfiguration
{
    public const string SectionName = "OpenTelemetry";
    public string TracingEndpoint { get; init; } = "";
}
