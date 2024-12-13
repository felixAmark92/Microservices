using ProductMicroservice.CommonInterfaces;
using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess.RepositoryInterfaces;

public interface IProductRepository : IRepository<Product, int>
{
	 
}