using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using RabbitMQ.Client;

namespace ServiceA;

public class RabbitMqProducer
{
    private readonly RabbitMqConnection _rabbitMqConnection;

    public RabbitMqProducer(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task SendMessage(InventoryDto message)
    {
        if (_rabbitMqConnection.Connection is null)
            await _rabbitMqConnection.InitializeConnection();
            
        var factory = new ConnectionFactory { HostName = "rabbit-mq" };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: "product-added", durable: false, exclusive: false, 
            autoDelete: true, arguments: null);
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, routingKey: "product-added", body: body );
    }
}