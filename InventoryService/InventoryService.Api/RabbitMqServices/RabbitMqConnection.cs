using RabbitMQ.Client;

namespace InventoryService.Api.RabbitMqServices;

public class RabbitMqConnection : IDisposable, IAsyncDisposable
{
    public IConnection? Connection { get; private set; }

    public async Task InitializeConnection()
    {
        if (Connection != null) return;
        
        var factory = new ConnectionFactory { HostName = "rabbit-mq" };
        Connection = await factory.CreateConnectionAsync();
    }
    public void Dispose()
    {
        Connection?.Dispose();
    }
    public async ValueTask DisposeAsync()
    {
        if (Connection != null) await Connection.DisposeAsync();
    }
}