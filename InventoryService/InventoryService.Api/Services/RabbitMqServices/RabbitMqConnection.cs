using RabbitMQ.Client;

namespace InventoryService.Api.Services.RabbitMqServices;

public class RabbitMqConnection : IDisposable, IAsyncDisposable
{
    public IConnection? Connection { get; private set; }
    private readonly ILogger<RabbitMqConnection> _logger;

    public RabbitMqConnection(ILogger<RabbitMqConnection> logger)
    {
        _logger = logger;
    }

    public async Task<bool> InitializeConnection()
    {
        if (Connection != null) return true;
        
        var factory = new ConnectionFactory { HostName = "rabbit-mq" };
        try
        {
            Connection = await factory.CreateConnectionAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to create connection");
            return false;
        }
        return true;
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