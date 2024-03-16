using eShop.API.DbContext;
using eShop.API.Endpoint;
using eShop.API.Service;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>(sp => new MongoDbContext(configuration));

builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddHostedService<EmailBackgroundService>();

var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQSettings");

builder.Services.AddSingleton<IConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = rabbitMQSettings["HostName"],
        UserName = rabbitMQSettings["UserName"],
        Password = rabbitMQSettings["Password"]
    };

    return factory.CreateConnection();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
