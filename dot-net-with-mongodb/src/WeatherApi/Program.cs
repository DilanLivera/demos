using MongoDB.Driver;
using WeatherApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddScoped<MongoClient>(serviceProvider =>
{
    var configration = serviceProvider.GetService<IConfiguration>();

    ArgumentNullException.ThrowIfNull(configration, nameof(configration));

    var mongoDbConnection = configration.GetSection("ConnectionStrings:MongoDatabase");

    return new MongoClient(mongoDbConnection.Value);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
