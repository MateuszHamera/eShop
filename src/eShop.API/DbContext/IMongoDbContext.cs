using eShop.API.Entity;
using MongoDB.Driver;

namespace eShop.API.DbContext;

public interface IMongoDbContext
{
    IMongoCollection<Product> Products { get; }
}