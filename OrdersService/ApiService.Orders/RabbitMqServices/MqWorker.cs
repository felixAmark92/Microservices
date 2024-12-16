namespace ApiService.Orders.RabbitMqServices;

public class MqWorker : BackgroundService
{
    private readonly ILogger<MqWorker> _logger;
    private readonly IRabbitMqContext _rabbitMqContext;
    
    public MqWorker(ILogger<MqWorker> logger, IRabbitMqContext rabbitMqContext )
    {
        _logger = logger;
        _rabbitMqContext = rabbitMqContext;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}