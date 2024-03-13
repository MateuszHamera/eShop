using eShop.API.Entity;
using MongoDB.Driver;

namespace eShop.API.DbContext;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDbSettings:ConnectionString"];
        var mongoClient = new MongoClient(connectionString);
        _database = mongoClient.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
    }

    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}