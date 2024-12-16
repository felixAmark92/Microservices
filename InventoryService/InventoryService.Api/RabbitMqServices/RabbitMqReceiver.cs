using RabbitMQ.Client.Events;
using ServiceA;

namespace InventoryService.Api.MessageQueue;

public class RabbitMqReceiver
{
    private readonly RabbitMqConnection _rabbitMqConnection;

    public RabbitMqReceiver(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task OnReceived(string queueName, Func<object, BasicDeliverEventArgs, Task> callback)
    {
        if (_rabbitMqConnection.Connection is null)
            await _rabbitMqConnection.InitializeConnection();
        
        await using var channel = await _rabbitMqConnection.Connection!.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(queueName, exclusive: false, autoDelete: true, durable: false);
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += callback.Invoke; 
        await channel.BasicConsumeAsync(
            queue: queueName, 
            autoAck: true, 
            consumer: consumer, 
            consumerTag: "ok", 
            noLocal: true, 
            exclusive: false, 
            arguments: null);
    }
    
    
}