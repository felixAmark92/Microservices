using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;

namespace InventoryService.Api.Service;

public interface IInventoryService
{
    public IResult AddInventory(InventoryDto inventoryDto);
    public IResult GetInventoryById(int id);
    public IResult GetAllInventories();
}