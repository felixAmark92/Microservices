using System.Text;
using System.Text.Json;
using InventoryService.Api.Dtos;
using InventoryService.Api.Services;
using InventoryService.Api.Services.RabbitMqServices;
using RabbitMQ.Client.Events;

namespace InventoryService.Api.Receivers;

public static class InventoryReceivers
{
    public static async Task<WebApplication> MapRabbitMqReceiver(this WebApplication app, RabbitMqReceiver rabbitMqReceiver)
    {
        
        await rabbitMqReceiver.OnReceived("product-added", async (obj, eventArgs) =>
        {
            //lidl dependency injection
            var inventoryService = app.Services.GetRequiredService<IInventoryService>();
            
            var newProduct = GetMessageDeserialized<InventoryDto>(eventArgs);
            await inventoryService.AddInventory(newProduct);
        });
        
        return app;
    }

    private static T GetMessageDeserialized<T>(BasicDeliverEventArgs ea)
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var result  = JsonSerializer.Deserialize<T>(message);
        return result;
    }
}