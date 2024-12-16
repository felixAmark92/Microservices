namespace InventoryService.Api.Dtos;

public record InventoryDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}