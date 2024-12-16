using WebShop;

namespace InventoryService.DataAccess.Entities;

public class InventoryEntity : IEntity<int>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}