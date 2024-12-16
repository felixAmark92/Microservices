using System.Text;
using RabbitMQ.Client;

namespace ApiService.Orders;

interface IMqProducer
{
    Task PublishMessage(string? message, string queueName);
}

public class MqProducer(RabbitMqContext rabbitMqContext) : IMqProducer
{
    public async Task PublishMessage(string? message, string queueName)
    {
        if (rabbitMqContext.Connection is null)
        {
            await rabbitMqContext.InitializeConnection();
        }
        
        var channel = await rabbitMqContext.Connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "queueName", durable: false, exclusive: false, autoDelete: true, arguments: null);
        var body = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
    }
}