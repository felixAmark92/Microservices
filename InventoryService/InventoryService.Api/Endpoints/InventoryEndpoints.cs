using InventoryService.Api.Service;
using InventoryService.DataAccess.Models;
using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;
using WebShop.UnitOfWork;

namespace InventoryService.Api.Endpoints;

public static class InventoryEndpoints
{
    public static WebApplication MapInventoryEndpoints(this WebApplication app)
    {
        app.MapPost("/inventory/insert", (InventoryDto inventoryDto, IInventoryService inventoryService) => 
            inventoryService.AddInventory(inventoryDto));
        
        app.MapGet("/inventory/{id}", (int id, IInventoryService inventoryService) => 
            inventoryService.GetInventoryById(id));
        
        app.MapGet("/inventory/all", (IInventoryService inventoryService) => 
            inventoryService.GetAllInventories());
        
        app.MapPost("/inventory/add-quantity", AddQuantity);
        app.MapPost("/inventory/remove-quantity", RemoveQuantity);
        return app;
    }
    
    private static async Task<IResult> AddQuantity(
        InventoryDto dto, 
        IInventoryService inventoryService)
    {
        return inventoryService.AddInventory(dto);
    }

    private static IResult RemoveQuantity(
        InventoryDto dto, 
        IInventoryRepository inventoryRepository, 
        IUnitOfWork unitOfWork)
    {
        var inventory = inventoryRepository.GetInventoryByProductId(dto.ProductId);
        if (inventory is null) 
            return Results.NotFound();
        
        inventoryRepository.RemoveFromInventoryQuantity(inventory, dto.Quantity);
        unitOfWork.CommitChanges();
        return Results.Ok();
    }
}