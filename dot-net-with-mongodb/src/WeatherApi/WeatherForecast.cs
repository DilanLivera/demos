using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WeatherApi;

public class WeatherForecast
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; private set; }
    public DateTime Date { get; private set; }
    public int TemperatureC { get; private set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; private set; }

    public WeatherForecast(DateTime date, int temperatureC, string? summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}
