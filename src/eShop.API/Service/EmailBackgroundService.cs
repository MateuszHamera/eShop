using System.Text;
using System.Text.Json;
using eShop.API.Message;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eShop.API.Service;

public class EmailBackgroundService : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public EmailBackgroundService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost", UserName = "user", Password = "password" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "orderEmails", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (moduleHandle, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var emailMessage = JsonSerializer.Deserialize<EmailMessage>(message);

            // Send email logic here

            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: "orderEmails", autoAck: false, consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}