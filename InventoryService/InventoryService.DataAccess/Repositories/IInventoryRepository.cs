using InventoryService.DataAccess.Models;
using WebShop.Repositories;

namespace InventoryService.DataAccess.Repositories;

public interface IInventoryRepository : IRepository<InventoryModel, int>
{
    void AddToInventory(InventoryModel inventory, int quantity );
    void RemoveFromInventory(InventoryModel inventory, int quantity );
    InventoryModel? GetInventoryByProductId(int productId);
}