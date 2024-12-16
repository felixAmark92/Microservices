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
        
        app.MapPost("/inventory/remove-quantity", (InventoryDto inventoryDto, IInventoryService inventoryService)=>
            inventoryService.RemoveFromProductQuantity(inventoryDto));
        
        app.MapPost("/inventory/add-quantity", (InventoryDto inventoryDto, IInventoryService inventoryService)=>
            inventoryService.AddToProductQuantity(inventoryDto));
        
        return app;
    }

}