using System.Text;
using System.Text.Json;
using ApiService.Orders.Dtos;
using ApiService.Orders.Services;

namespace ApiService.Orders.RabbitMqServices;

public class OrderConsumerFactory(IServiceProvider serviceProvider, IRabbitMqContext rabbitMqContext)
{
    public IMqOrderConsumer CreateOrderConsumer(string queueName)
    {
        return queueName switch
        {
            "orders.created" => new MqOrderConsumer(serviceProvider, rabbitMqContext, queueName, async (scope, message) =>
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService<OrderDto>>();
                try
                {
                    var orderDto = JsonSerializer.Deserialize<OrderDto>(message);
                    if(orderDto is null)
                    {
                        throw new InvalidOperationException("Invalid order");
                    }
                    await orderService.AddAsync(orderDto);
                }
                catch (Exception e)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<MqOrderConsumer>>();
                    logger.LogError(e, "Failed to process order");
                }
            }),
            "orders.updated" => new MqOrderConsumer(serviceProvider, rabbitMqContext, queueName, async (scope, message) =>
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService<OrderDto>>();
                var orderDto = JsonSerializer.Deserialize<OrderDto>(message);
                if(orderDto is null)
                {
                    throw new InvalidOperationException("Invalid order");
                }
                await orderService.UpdateAsync(orderDto);
            }),
            "orders.deleted" => new MqOrderConsumer(serviceProvider, rabbitMqContext, queueName, async (scope, message) =>
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService<OrderDto>>();
                var orderId = JsonSerializer.Deserialize<string>(message);
                if(orderId is null)
                {
                    throw new InvalidOperationException("Invalid order");
                }
                await orderService.DeleteAsync(orderId);
            }),
            _ => throw new InvalidOperationException("Invalid queue name")
        };
    }
}