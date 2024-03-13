using eShop.API.DbContext;
using eShop.API.Dto;
using eShop.API.Entity;
using eShop.API.Mapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>(sp => new MongoDbContext(configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/products", async ([FromServices] IMongoDbContext dbContext) =>
{
    return await dbContext.Products.Find(_ => true).ToListAsync();
});

app.MapPost("/products", async (IMongoDbContext dbContext, CreateUpdateProductDto productDto) =>
{
    await dbContext.Products.InsertOneAsync(productDto.MapToEntity());
    return Results.Created();
});

app.Run();
