using InventoryService.DataAccess.Models;
using WebShop.UnitOfWork;

namespace InventoryService.DataAccess.Repositories;

public class InventoryRepository : BaseRepository<InventoryModel, int>, IInventoryRepository
{
    private readonly InventoryDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryRepository(IUnitOfWork unitOfWork, InventoryDbContext context)
        : base(unitOfWork, context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public void AddToInventory(InventoryModel inventory, int quantity)
    {
        Action operation = () =>
        {
            inventory.InventoryAmount += quantity;
            _context.Set<InventoryModel>().Update(inventory);
        };
        _unitOfWork.AddOperation(operation);
    }

    public void RemoveFromInventory(InventoryModel inventory, int quantity)
    {
        Action operation = () =>
        {
            inventory.InventoryAmount -= quantity;
            if (inventory.InventoryAmount < 0) 
                throw new NullReferenceException("Inventory amount cannot be negative");
                
            _context.Set<InventoryModel>().Update(inventory);
        };
        _unitOfWork.AddOperation(operation);
    }

    public InventoryModel? GetInventoryByProductId(int productId)
    {
        return _context.Set<InventoryModel>().FirstOrDefault(i => i.ProductId == productId);
    }
}