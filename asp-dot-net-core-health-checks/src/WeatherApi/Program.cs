using System.Text.Json;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherApi;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecks()
    .AddCheck<MemoryHealthCheck>(
        name: "memory",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "memory" });

WebApplication? app = builder.Build();

app.UseHttpsRedirection();

app.MapHealthChecks(
    pattern: "/ready",
    options: new HealthCheckOptions
    {
        ResponseWriter = WriteHealthCheckResponseAsync,
        ResultStatusCodes =
        {
            [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
        },
        Predicate = registration => registration.Tags.Contains("memory")
    });

app.MapHealthChecks(
    pattern: "/healthui",
    options: new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    });

app.Run();

static Task WriteHealthCheckResponseAsync(HttpContext httpContext, HealthReport healthReport)
{
    httpContext.Response.ContentType = "application/json";

    var dependencyHealthChecks = healthReport.Entries.Select(entry => new
    {
        Name = entry.Key,
        Discription = entry.Value.Description,
        Status = entry.Value.Status.ToString(),
        DurationInSeconds = entry.Value.Duration.TotalSeconds.ToString("0:0.00"),
        Data = entry.Value.Data,
        Exception = entry.Value.Exception?.Message
    });

    var healthCheckResponse = new
    {
        Status = healthReport.Status.ToString(),
        TotalCheckExecutionTimeInSeconds = healthReport.TotalDuration.TotalSeconds.ToString("0:0.00"),
        DependencyHealthChecks = dependencyHealthChecks
    };

    var serializerOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var responseString = JsonSerializer.Serialize(healthCheckResponse, serializerOptions);

    return httpContext.Response.WriteAsync(responseString);
}
