
namespace SmartCafeteria.ProductMicroservice.DataAccess.Entities;

public interface IProduct : IEntity<int>
{
	public int Id { get; set; }
	
}