namespace ApiService.Orders.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    public float TotalPrice { get; set; } 
    public ICollection<int> ProductIds { get; set; } = new List<int>();
}