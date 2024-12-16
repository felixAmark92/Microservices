using InventoryService.Api.Dtos;
using InventoryService.Api.Services;
using InventoryService.DataAccess.Repositories;

namespace InventoryService.Api.Endpoints;

public static class InventoryEndpoints
{
    public static WebApplication MapInventoryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/inventory");
        
        group.MapPost("/insert", (InventoryDto inventoryDto, IInventoryService inventoryService) => 
            inventoryService.AddInventory(inventoryDto));
        
        group.MapGet("/{id}", (int id, IInventoryService inventoryService) => 
            inventoryService.GetInventoryById(id));
        
        group.MapGet("/all", (IInventoryService inventoryService) => 
            inventoryService.GetAllInventories());
        
        group.MapPost("/remove-quantity", (InventoryDto inventoryDto, IInventoryService inventoryService)=>
            inventoryService.RemoveFromProductQuantity(inventoryDto));
        
        group.MapPost("/add-quantity", (InventoryDto inventoryDto, IInventoryService inventoryService)=>
            inventoryService.AddToProductQuantity(inventoryDto));
        
        return app;
    }

}