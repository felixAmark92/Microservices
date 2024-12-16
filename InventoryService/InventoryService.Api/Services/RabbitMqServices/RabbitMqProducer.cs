using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace InventoryService.Api.Services.RabbitMqServices;

public class RabbitMqProducer
{
    private readonly RabbitMqConnection _rabbitMqConnection;

    public RabbitMqProducer(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task SendMessage(string message, string queueName)
    {
        if (_rabbitMqConnection.Connection is null)
            await _rabbitMqConnection.InitializeConnection();
            
        await using var channel = await _rabbitMqConnection.Connection!.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, 
            autoDelete: true, arguments: null);
        
        var body = Encoding.UTF8.GetBytes(message);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, routingKey: queueName, body: body );
    }

    public async Task SendJson<T>(T obj, string queueName)
    {
        if (_rabbitMqConnection.Connection is null)
            await _rabbitMqConnection.InitializeConnection();
            
        await using var channel = await _rabbitMqConnection.Connection!.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, 
            autoDelete: true, arguments: null);
        var jsonString = JsonSerializer.Serialize(obj);
        
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, routingKey: queueName, body: body );
    }
}