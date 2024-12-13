using System.Text;
using RabbitMQ.Client;

namespace FeedbackService;

public class RabbitMqProducer
{
    private readonly RabbitMqConnection _rabbitMqConnection;

    public RabbitMqProducer(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task SendMessage(string message)
    {
        if (_rabbitMqConnection.Connection is null)
            await _rabbitMqConnection.InitializeConnection();
            
        var factory = new ConnectionFactory { HostName = "rabbit-mq" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: "standard", durable: false, exclusive: false, 
            autoDelete: true, arguments: null);
        
        var body = Encoding.UTF8.GetBytes(message);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, routingKey: "standard", body: body );
    }
}