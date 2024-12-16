using InventoryService.Api.MessageQueue;
using InventoryService.DataAccess;
using InventoryService.DataAccess.Repositories;
using ServiceA;
using WebShop.UnitOfWork;

namespace InventoryService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IInventoryRepository, InventoryRepository>()
            .AddSingleton<RabbitMqConnection>()
            .AddScoped<RabbitMqProducer>()
            .AddSingleton<RabbitMqReceiver>();
            
        return services;
    }
}