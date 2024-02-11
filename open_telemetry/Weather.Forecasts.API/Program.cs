using Common.OpenTelemetry;
using Weather.Forecasts.API.WeatherForecasts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomOpenTelemetry(
    builder.Configuration,
    builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGetWeatherForecasts();

app.Run();
