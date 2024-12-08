using System.Text;
using RabbitMQ.Client;

namespace ServiceA;

public class RabbitMqProducer
{
    private readonly RabbitMqConnection _rabbitMqConnection;

    public RabbitMqProducer(RabbitMqConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    public async Task SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        if (_rabbitMqConnection.Channel == null) throw new Exception("RabbitMQ channel is null");
        
        await _rabbitMqConnection.Channel.BasicPublishAsync(
            exchange: string.Empty, routingKey: "standard", body: body );
    }
}