using System.Text;
using System.Text.Json;
using InventoryService.Api.Dtos;
using InventoryService.Api.RabbitMqServices;
using InventoryService.Api.Service;
using RabbitMQ.Client.Events;

namespace InventoryService.Api.Receivers;

public static class InventoryReceivers
{
    public static async Task<WebApplication> MapRabbitMqReceiver(this WebApplication app, RabbitMqReceiver rabbitMqReceiver)
    {
        //lidl dependency injection
        var inventoryService = app.Services.GetRequiredService<IInventoryService>();
        
        await rabbitMqReceiver.OnReceived("product-added", async (o, args) =>
        {
            var newProduct = GetMessageDeserialized<InventoryDto>(args);
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