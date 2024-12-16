using RabbitMQ.Client;

namespace WorkerService.Orders;

public interface IRabbitMqContext
{
    IConnection? Connection { get; }
    Task InitializeConnection();
}

public class RabbitMqContext: IDisposable, IAsyncDisposable, IRabbitMqContext
{
    private readonly ConnectionFactory _factory;
    public IConnection? Connection { get; private set; }
    
    public RabbitMqContext(string hostName = "rabbit-mq")
    {
        _factory = new ConnectionFactory { HostName = hostName };
    }
    
    public async Task InitializeConnection()
    {
        if (Connection != null) return;
        
        try
        {
            Connection = await _factory.CreateConnectionAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Failed to create RabbitMQ connection", e);
        }
        
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (Connection != null) await Connection.DisposeAsync().ConfigureAwait(false);
    }
}