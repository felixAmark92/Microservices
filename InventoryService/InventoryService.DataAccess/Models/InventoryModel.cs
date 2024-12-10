using WebShop;

namespace InventoryService.DataAccess.Models;

public class InventoryModel : IModel<int>
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int InventoryAmount { get; set; }
}