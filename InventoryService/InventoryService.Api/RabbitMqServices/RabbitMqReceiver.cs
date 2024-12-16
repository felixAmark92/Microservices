using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceA;

namespace InventoryService.Api.MessageQueue;

public class RabbitMqReceiver
{
    private readonly IChannel _channel;

    public RabbitMqReceiver(IChannel channel)
    {
        _channel = channel;
    }

    public async Task OnReceived( string queueName, Func<object, BasicDeliverEventArgs, Task> callback)
    {
        await _channel.QueueDeclareAsync(queueName, exclusive: false, autoDelete: true, durable: false);
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += callback.Invoke; 
        await _channel.BasicConsumeAsync(
            queue: queueName, 
            autoAck: true, 
            consumer: consumer, 
            consumerTag: "ok", 
            noLocal: true, 
            exclusive: false, 
            arguments: null);
    }
}