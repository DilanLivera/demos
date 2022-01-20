using MongoDB.Driver;

namespace WeatherApi;

public interface IWeatherForecastRepository
{
    Task<IReadOnlyList<WeatherForecast>> GetAsync(CancellationToken cancellationToken = default);
    Task<WeatherForecast> GetAsync(string id, CancellationToken cancellationToken = default);
    Task AddAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken = default);
}

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private const string DatabaseName = "dotnet_with_mongodb_demo_db";
    private const string CollectionName = nameof(WeatherForecast);
    private readonly IMongoCollection<WeatherForecast> _collection;

    public WeatherForecastRepository(MongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(DatabaseName);
        _collection = database.GetCollection<WeatherForecast>(CollectionName);
    }

    public Task AddAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(weatherForecast));

        var insertOptions = new InsertOneOptions();

        return _collection.InsertOneAsync(weatherForecast, insertOptions, cancellationToken);
    }

    public async Task<WeatherForecast> GetAsync(string id, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException($"{nameof(id)} can not be null or empty", nameof(id));
        }
        var idFilter = Builders<WeatherForecast>.Filter.Eq(nameof(WeatherForecast.Id), id);

        var findOptions = new FindOptions<WeatherForecast, WeatherForecast>();
        var weatherForecasts = await _collection.FindAsync(idFilter, findOptions, cancellationToken);

        return await weatherForecasts.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WeatherForecast>> GetAsync(CancellationToken cancellationToken)
    {
        var findOptions = new FindOptions<WeatherForecast, WeatherForecast>();
        var weatherForecasts = await _collection.FindAsync(weatherForecast => true, findOptions, cancellationToken);

        return await weatherForecasts.ToListAsync(cancellationToken);
    }
}
