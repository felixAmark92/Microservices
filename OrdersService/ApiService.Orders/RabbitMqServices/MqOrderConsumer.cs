using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using ApiService.Orders.Dtos;
using ApiService.Orders.Services;

namespace ApiService.Orders.RabbitMqServices;

public interface IMqOrderConsumer
{
    Task StartListening();
}
public class MqOrderConsumer(
    IServiceProvider serviceProvider,
    IRabbitMqContext rabbitMqContext,string queueName,
    Func<IServiceScope, string, Task> processOrder 
    ) : IMqOrderConsumer
{
    public async Task StartListening()
    {
        if (rabbitMqContext.Connection is null)
        {
            await rabbitMqContext.InitializeConnection();
        }
        
        var channel = await rabbitMqContext.Connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "orderQueue", durable: false, exclusive: false, autoDelete: true, arguments: null);
        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (model, ea) =>
        {
            using var scope = serviceProvider.CreateScope();
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await processOrder(scope, message);
            }
            catch (Exception e)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<MqOrderConsumer>>();
                logger.LogError(e, "Failed to process order");
            }
        };
        
        await channel.BasicConsumeAsync(queue: "orderQueue", autoAck: true, consumer: consumer);
    }
}
