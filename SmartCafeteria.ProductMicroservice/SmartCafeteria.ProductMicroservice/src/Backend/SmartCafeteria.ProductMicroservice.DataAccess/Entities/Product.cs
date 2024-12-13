

namespace SmartCafeteria.ProductMicroservice.DataAccess.Entities;

public class Product : IProduct
{
	public int Id { get; set; }
	public double Price { get; set; }
	public string Name { get; set; }
	public bool IsAvailable { get; set; }
	
}