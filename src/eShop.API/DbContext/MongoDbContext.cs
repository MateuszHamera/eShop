using eShop.API.Entity;
using MongoDB.Driver;

namespace eShop.API.DbContext;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        _database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}