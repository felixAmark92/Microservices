namespace ApiService.Orders.RabbitMqServices;

public class OrderConsumerManager
{
    private readonly List<IMqOrderConsumer> _consumers = new();
    
    public OrderConsumerManager(OrderConsumerFactory orderConsumerFactory)
    {
        _consumers.Add(orderConsumerFactory.CreateOrderConsumer("orders.created"));
        _consumers.Add(orderConsumerFactory.CreateOrderConsumer("orders.updated"));
        _consumers.Add(orderConsumerFactory.CreateOrderConsumer("orders.deleted"));
    }
    
    public async Task StartConsumers()
    {
        foreach (var consumer in _consumers)
        {
            await consumer.StartListening();
        }
    }
}
