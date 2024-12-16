using InventoryService.Api.Endpoints;
using InventoryService.Api.Extensions;
using InventoryService.Api.MessageQueue;
using InventoryService.Api.Receivers;
using InventoryService.DataAccess;
using Microsoft.EntityFrameworkCore;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
var logger = loggerFactory.CreateLogger<Program>();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("InventoryService_ConnectionString");
if (string.IsNullOrWhiteSpace(connectionString))
{
    logger.LogWarning("Could not find environment variable 'InventoryService_ConnectionString'. Searching in configuration files instead");
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
builder.Services.AddDbContext<InventoryDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddApplicationServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    try
    {
        await dbContext.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        logger.LogError(e, "An error occurred while migrating the database.");
    }
}

app.MapInventoryEndpoints();
var rabbitMqReceiver = app.Services.GetRequiredService<RabbitMqReceiver>();
await app.MapRabbitMqReceiver(rabbitMqReceiver);
app.Run();

