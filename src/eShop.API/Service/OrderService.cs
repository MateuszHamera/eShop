using System.Text;
using System.Text.Json;
using eShop.API.Entity;
using eShop.API.Message;
using RabbitMQ.Client;

namespace eShop.API.Service;

public class OrderService : IOrderService
{
    private readonly IModel _channel;

    public OrderService(IConnection connection)
    {
        _channel = connection.CreateModel();
        _channel.QueueDeclare(queue: "orderEmails", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public void PublishOrderConfirmationEmail(Order order)
    {
        var emailMessage = new EmailMessage
        {
            OrderId = order.Id,
            To = order.Email,
            Subject = "Your order has been placed",
            Body = $"Thank you for your order, {order.Id}."
        };
    
        var message = JsonSerializer.Serialize(emailMessage);
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "", routingKey: "orderEmails", basicProperties: null, body: body);
    }
}