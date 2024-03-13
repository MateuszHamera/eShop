using eShop.API.Entity;
using GraphQL.Types;

namespace eShop.API.GraphQL.ObjectGraphType;

public class ProductType : ObjectGraphType<Product>, IGraphType
{
    public ProductType()
    {
        Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the product.");
        Field(x => x.Name).Description("The name of the product.");
        Field(x => x.Price).Description("The price of the product.");
    }
}