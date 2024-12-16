namespace InventoryService.DataAccess;

public interface IEntity<TId>
{
    public TId Id { get; set; } 
}