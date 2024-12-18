using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartCafeteria.ProductMicroservice.DataAccess.Entities;
using SmartCafeteria.ProductMicroservice.DataAccess.RepositoryInterfaces;

namespace SmartCafeteria.ProductMicroservice.DataAccess.Repositories;

public class ProductRepository(ProductServiceDbContext context) : IProductRepository
{
	private readonly ILogger<ProductRepository> logger;
	public async Task<Product> GetByIdAsync(int id)
	{
		var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

		try
		{
			if (product is null)
			{
				logger.LogWarning($"Product with ID {id} not found.");
				return new Product();
			}

			
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error in GetByIdAsync");
			
		}

		return product;

	}

	public async Task<IEnumerable<Product>> GetAllAsync()
	{
		var products = await context.Products.ToListAsync();
		return products;
	}

	public async Task AddAsync(Product entity)
	{
		await context.Products.AddAsync(entity);
	}

	public async Task UpdateAsync(Product entity, int id)
	{
		try
		{
			var productToUpdate = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

			if (productToUpdate is null)
			{
				logger.LogWarning($"Product with ID {id} not found for update.");
				return;
			}

			productToUpdate.Name = entity.Name;
			productToUpdate.Price = entity.Price;
			productToUpdate.IsAvailable = entity.IsAvailable;

		}
		catch (Exception ex)
		{
			logger.LogError(ex, $"An error occurred while updating the product with ID {id}");
		}
	}


	public async Task DeleteAsync(int id)
	{
		try
		{
			var productToDelete = await context.Products.FirstOrDefaultAsync(p => p.Id == id);

			if (productToDelete is null)
			{
				logger.LogWarning($"Product with ID {id} not found for delete.");
				return;
			}

			var entityEntry = context.Products.Update(productToDelete);
			entityEntry.Property(p => p.IsAvailable).CurrentValue = false;

		}
		catch (Exception ex)
		{
			logger.LogError($"An error occured while deleting the product with ID {id}");
		}
	}
}