using Microsoft.EntityFrameworkCore;
using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace SmartCafeteria.ProductMicroservice.DataAccess;

public class ProductServiceDbContext : DbContext
{
	public DbSet<Product> Products { get; set; }
	public ProductServiceDbContext(DbContextOptions<ProductServiceDbContext> options) : base(options)
	{

	}
}