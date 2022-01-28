# .Net with MongoDB

First download and install MongoDB locally. Link to [MongoDB Community Server](https://www.mongodb.com/try/download/community)

Then, we need to add [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver/) package to our project.

After that, we can create a `MongoClient` and connect to a database to work with a document collection.

```csharp
var mongoClient = new MongoClient();
var database = mongoClient.GetDatabase(DatabaseName);
IMongoCollection<WeatherForecast> collection = database.GetCollection<WeatherForecast>(CollectionName);

//Add to a collection
var insertOptions = new InsertOneOptions();
collection.InsertOneAsync(weatherForecast, insertOptions, cancellationToken);

//Get all
FindOptions<WeatherForecast, WeatherForecast>? findOptions = new FindOptions<WeatherForecast, WeatherForecast>();
IAsyncCursor<WeatherForecast>? weatherForecasts = await _collection.FindAsync(weatherForecast => true, findOptions, cancellationToken);

//Get by filter eg. id
FilterDefinition<WeatherForecast>? idFilter = Builders<WeatherForecast>.Filter.Eq(nameof(WeatherForecast.Id), id);
var findOptions = new FindOptions<WeatherForecast, WeatherForecast>();
IAsyncCursor<WeatherForecast>? weatherForecasts = await _collection.FindAsync(idFilter, findOptions, cancellationToken);
```

## Credits

- [How to Connect MongoDB to C# the Easy Way](https://www.youtube.com/watch?v=exXavNOqaVo)

## Resources

- [MongoDB Community Server](https://www.mongodb.com/try/download/community)
- [MongoDB C#/.NET Driver](https://docs.mongodb.com/drivers/csharp/)
