using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Common.OpenTelemetry;

public static class OpenTelemetryServiceRegistration
{
    public static IServiceCollection AddCustomOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services
            .AddOptions<OpenTelemetryConfiguration>()
            .Bind(configuration.GetSection(OpenTelemetryConfiguration.SectionName));

        var config = services.BuildServiceProvider()
            .GetRequiredService<IOptions<OpenTelemetryConfiguration>>()
            .Value;

        var openTelemetry = services.AddOpenTelemetry();

        openTelemetry.ConfigureResource(b =>
        {
            b.AddService(
                serviceName: environment.ApplicationName);
        });

        openTelemetry.WithTracing(b =>
        {
            b.AddAspNetCoreInstrumentation();

            if (string.IsNullOrWhiteSpace(config.TracingEndpoint))
            {
                b.AddConsoleExporter();
            }
            else
            {
                b.AddOtlpExporter(o => o.Endpoint = new Uri(config.TracingEndpoint));
            }
        });

        openTelemetry.WithMetrics(b =>
        {
            b.AddAspNetCoreInstrumentation();
            b.AddRuntimeInstrumentation();
            b.AddConsoleExporter();
        });

        return services;
    }
}
