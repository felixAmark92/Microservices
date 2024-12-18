namespace SmartCafeteria.ProductMicroservice.DataAccess.Entities;

public interface IEntity<T>
{
	T Id { get; set; }
}