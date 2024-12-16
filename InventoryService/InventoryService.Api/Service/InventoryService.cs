using InventoryService.DataAccess.Models;
using InventoryService.DataAccess.Repositories;
using InventoryService.Dtos;
using WebShop.UnitOfWork;

namespace InventoryService.Api.Service;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<InventoryService> _logger;
    private readonly IInventoryRepository _repository;

    public InventoryService(IUnitOfWork unitOfWork, ILogger<InventoryService> logger, IInventoryRepository repository)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _repository = repository;
    }

    public async Task<IResult> AddInventory(InventoryDto inventoryDto)
    {
        var entity = new InventoryEntity()
        {
            ProductId = inventoryDto.ProductId,
            Quantity = inventoryDto.Quantity,
        };
        _repository.Add(entity);
        await _unitOfWork.CommitChanges();
        return Results.Ok(entity);
    }

    public IResult GetInventoryById(
        int id)
    {
        var inventory = _repository.GetById(id);
        if (inventory is null ) 
            return Results.NotFound();

        var dto = new InventoryDto()
        {
            ProductId = inventory.ProductId,
            Quantity = inventory.Quantity,
        };
        return Results.Ok(dto);
    }
    
    public async Task<IResult> AddToProductQuantity(InventoryDto inventoryDto)
    {
        var inventory = _repository.GetInventoryByProductId(inventoryDto.ProductId);
        if (inventory is null) 
            return Results.NotFound();
        _repository.AddToInventoryQuantity(inventory, inventoryDto.Quantity);
        await _unitOfWork.CommitChanges();
        return Results.Ok();
    }

    public Task<IResult> RemoveFromProductQuantity(InventoryDto inventoryDto)
    {
        throw new NotImplementedException();
    }

    public IResult GetAllInventories()
    {
        var inventories = _repository.GetAll();
        var dtos = inventories.Select(i => new InventoryDto
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
        });
        return Results.Ok(dtos);
    }
}