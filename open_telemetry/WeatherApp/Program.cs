using Common.OpenTelemetry;
using WeatherApp.Components;
using WeatherApp.Components.Pages.WeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddWeatherForecasts(
    builder.Configuration);

builder.Services.AddCustomOpenTelemetry(
    builder.Configuration,
    builder.Environment);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
