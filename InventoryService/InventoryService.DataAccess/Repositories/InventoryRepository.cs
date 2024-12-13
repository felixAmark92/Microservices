using InventoryService.DataAccess.Models;
using WebShop.UnitOfWork;

namespace InventoryService.DataAccess.Repositories;

public class InventoryRepository : BaseRepository<InventoryEntity, int>, IInventoryRepository
{
    private readonly InventoryDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public InventoryRepository(IUnitOfWork unitOfWork, InventoryDbContext context)
        : base(unitOfWork, context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public void AddToInventoryQuantity(InventoryEntity inventory, int quantity)
    {
        Action operation = () =>
        {
            inventory.Quantity += quantity;
            _context.Set<InventoryEntity>().Update(inventory);
        };
        _unitOfWork.AddOperation(operation);
    }

    public void RemoveFromInventoryQuantity(InventoryEntity inventory, int quantity)
    {
        Action operation = () =>
        {
            inventory.Quantity -= quantity;
            if (inventory.Quantity < 0) 
                throw new NullReferenceException("Inventory amount cannot be negative");
                
            _context.Set<InventoryEntity>().Update(inventory);
        };
        _unitOfWork.AddOperation(operation);
    }

    public InventoryEntity? GetInventoryByProductId(int productId)
    {
        return _context.Set<InventoryEntity>().FirstOrDefault(i => i.ProductId == productId);
    }
}