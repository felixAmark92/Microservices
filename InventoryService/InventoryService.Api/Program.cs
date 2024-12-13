using InventoryService.DataAccess;
using InventoryService.DataAccess.Models;
using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;
using Microsoft.EntityFrameworkCore;
using WebShop.UnitOfWork;

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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    if(!dbContext.Database.IsInMemory())
        dbContext.Database.Migrate();
}

app.MapGet("/", () => "Hello world");

//should be replaced with an rabbitMQ whenever a new product is added
app.MapPost("/inventory/insert", (InventoryDto dto, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork) =>
{
    var inventoryEntity = new InventoryEntity()
    {
        Quantity = dto.Quantity,
        ProductId = dto.ProductId,
    };
    inventoryRepository.Add(inventoryEntity);
    unitOfWork.CommitChanges();
});

app.MapPost("/inventory/add-quantity", (InventoryDto dto, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)=>
{
    var inventory = inventoryRepository.GetInventoryByProductId(dto.ProductId);
    if (inventory is null)
        return Results.NotFound();
    inventoryRepository.AddToInventoryQuantity(inventory, dto.Quantity);
    unitOfWork.CommitChanges();
    return Results.Ok();
});

app.MapPost("/inventory/remove-quantity", (InventoryDto dto, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)=>
{
    var inventory = inventoryRepository.GetInventoryByProductId(dto.ProductId);
    if (inventory is null)
        return Results.NotFound();
    inventoryRepository.RemoveFromInventoryQuantity(inventory, dto.Quantity);
    unitOfWork.CommitChanges();
    return Results.Ok();
});

app.Run();