namespace Repository.Orders.Entities;

public class OrderEntity
{
    public int Id { get; set; }
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
    public float TotalPrice { get; set; } 
}