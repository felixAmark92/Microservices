using InventoryService.DataAccess;
using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;
using Microsoft.EntityFrameworkCore;
using WebShop.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("InventoryService_ConnectionString");
if (string.IsNullOrWhiteSpace(connectionString))
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is null) throw new ArgumentNullException();
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlServer(connectionString));

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

app.MapPost("/inventory/add", (ChangeInventoryDto dto, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)=>
{
    var inventory = inventoryRepository.GetInventoryByProductId(dto.ProductId);
    if (inventory is null)
        return Results.NotFound();
    inventoryRepository.AddToInventory(inventory, dto.Quantity);
    unitOfWork.CommitChanges();
    return Results.Ok();
});

app.MapPost("/inventory/remove", (ChangeInventoryDto dto, IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork)=>
{
    var inventory = inventoryRepository.GetInventoryByProductId(dto.ProductId);
    if (inventory is null)
        return Results.NotFound();
    inventoryRepository.AddToInventory(inventory, dto.Quantity);
    unitOfWork.CommitChanges();
    return Results.Ok();
});

app.Run();