using InventoryService.DataAccess.Models;
using WebShop.Repositories;

namespace InventoryService.DataAccess.Repositories;

public interface IInventoryRepository : IRepository<InventoryEntity, int>
{
    void AddToInventoryQuantity(InventoryEntity inventory, int quantity );
    void RemoveFromInventoryQuantity(InventoryEntity inventory, int quantity );
    InventoryEntity? GetInventoryByProductId(int productId);
}