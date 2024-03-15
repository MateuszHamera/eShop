using eShop.API.Endpoint.Product;

namespace eShop.API.Endpoint;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/product").MapProductEndpoints();
    }
}