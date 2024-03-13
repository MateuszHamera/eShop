using eShop.API.DbContext;
using GraphQL.Types;
using MongoDB.Driver;

namespace eShop.API.GraphQL.ObjectGraphType;

public class EShopQuery : global::GraphQL.Types.ObjectGraphType
{
    public EShopQuery(IMongoDbContext dbContext)
    {
        Field<ListGraphType<ProductType>>(
            "products",
            resolve: context =>
            {
                var products = dbContext.Products.Find(_ => true).ToList();
                return products;
            });
    }
}