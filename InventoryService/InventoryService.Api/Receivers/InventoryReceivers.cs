using System.Text;
using System.Text.Json;
using InventoryService.Api.MessageQueue;
using InventoryService.Api.Service;
using InventoryService.DataAccess.Models;
using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;
using RabbitMQ.Client.Events;
using WebShop.UnitOfWork;

namespace InventoryService.Api.Receivers;

public static class InventoryReceivers
{
    public static async Task<WebApplication> MapRabbitMqReceiver(this WebApplication app, RabbitMqReceiver rabbitMqReceiver)
    {

        await rabbitMqReceiver.OnReceived("product-added", (o, args) =>
        {
            var inventoryService = app.Services.GetRequiredService<IInventoryService>();
            var newProduct = GetMessageDeserialized<InventoryDto>(args);
            inventoryService.AddInventory(newProduct);
            return Task.CompletedTask;
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