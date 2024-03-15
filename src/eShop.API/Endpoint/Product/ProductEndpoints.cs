using eShop.API.DbContext;
using eShop.API.Dto;
using eShop.API.Mapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Driver;

namespace eShop.API.Endpoint.Product;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetAllAsync);
        group.MapGet("/{id}", GetByIdAsync);
        group.MapPost("/", CreateProductAsync);
        group.MapPut("/{id}", UpdateProductAsync);
        group.MapDelete("/{id}", DeleteProductAsync);
    }

    private static async Task<Ok<IEnumerable<ProductDto>>> GetAllAsync(IMongoDbContext dbContext)
    {
        var result = await dbContext.Products.Find(_ => true).ToListAsync();

        var dtos = result.Select(x => x.MapToDto());
        
        return TypedResults.Ok(dtos);
    }

    private static async Task<Results<Ok<ProductDto>, NotFound>> GetByIdAsync(IMongoDbContext dbContext, string id)
    {
        var product = await dbContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (product is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(product.MapToDto());
    }
    
    private static async Task<Created<ProductDto>> CreateProductAsync(IMongoDbContext dbContext, ProductDto productDto)
    {
        var product = productDto.MapToEntity();
        await dbContext.Products.InsertOneAsync(product);
        
        return TypedResults.Created($"/products/{product.Id}", product.MapToDto());
    }
    
    private static async Task<Results<NoContent, NotFound>> UpdateProductAsync(IMongoDbContext dbContext, string id, ProductDto productDto)
    {
        var updateResult = await dbContext.Products.ReplaceOneAsync(p => p.Id == id, productDto.MapToEntity());

        if (updateResult.MatchedCount == 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.NoContent();
    }

    private static async  Task<Results<NoContent, NotFound>> DeleteProductAsync(IMongoDbContext dbContext, string id)
    {
        var deleteResult = await dbContext.Products.DeleteOneAsync(p => p.Id == id);

        if (deleteResult.DeletedCount == 0)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.NoContent();
    }
}