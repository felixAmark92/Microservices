namespace SmartCafeteria.ProductMicroservice.DataAccess.Data.DTOs.Product;

public class ProductDto 
{
	public int Id { get; set; }
	public double Price { get; set; }
	public string Name { get; set; }
	public bool IsAvailable { get; set; }
}