using RabbitMQ.Client;

namespace ServiceA;

public class RabbitMqConnection : IDisposable, IAsyncDisposable
{
    public IConnection? Connection { get; private set; }
    public IChannel? Channel { get; private set; }
    public RabbitMqConnection()
    {
        _ = InitializeConnection();
    }

    private async Task InitializeConnection()
    {
        var factory = new ConnectionFactory { HostName = "rabbit-mq" };
        Connection = await factory.CreateConnectionAsync();
        Channel = await Connection.CreateChannelAsync();
        
        await Channel.QueueDeclareAsync(queue: "standard", durable: false, exclusive: false, 
            autoDelete: false, arguments: null);
    }

    public void Dispose()
    {
        Connection?.Dispose();
        Channel?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (Connection != null) await Connection.DisposeAsync();
        if (Channel != null) await Channel.DisposeAsync();
    }
}