using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;

namespace InventoryService.Api.Service;

public interface IInventoryService
{
    public Task<IResult> AddInventory(InventoryDto inventoryDto);
    public IResult GetInventoryById(int id);
    public IResult GetAllInventories();
    public Task<IResult> AddToProductQuantity(InventoryDto inventoryDto);
    public Task<IResult> RemoveFromProductQuantity(InventoryDto inventoryDto);
}