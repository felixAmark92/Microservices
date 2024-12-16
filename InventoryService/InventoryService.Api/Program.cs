using InventoryService.Api.Endpoints;
using InventoryService.Api.Extensions;
using InventoryService.Api.Receivers;
using InventoryService.Api.Services.RabbitMqServices;
using InventoryService.DataAccess;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

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

app.MapOpenApi();
app.MapScalarApiReference();
app.MapInventoryEndpoints();

var rabbitConnection = app.Services.GetRequiredService<RabbitMqConnection>();
var success = await rabbitConnection.InitializeConnection();
if (!success)
{
    logger.LogWarning("Starting application without RabbitMQ connection");
    app.Run();
    return;
}
await using var channel = await rabbitConnection.Connection!.CreateChannelAsync();
var rabbitMqReceiver = new RabbitMqReceiver(channel);
await app.MapRabbitMqReceiver(rabbitMqReceiver);
app.Run();

