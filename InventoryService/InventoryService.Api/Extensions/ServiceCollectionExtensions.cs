using InventoryService.Api.RabbitMqServices;
using InventoryService.Api.Service;
using InventoryService.DataAccess;
using InventoryService.DataAccess.Repositories;

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
            .AddScoped<IInventoryService, Service.InventoryService>();
            
        return services;
    }
}