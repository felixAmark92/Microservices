using Microsoft.AspNetCore.Mvc;
using SmartCafeteria.ProductMicroservice.DataAccess.Entities;
using SmartCafeteria.ProductMicroservice.DataAccess.UnitOfWork;

namespace SmartCafeteria.ProductMicroservice.Api.Controllers
{

	[ApiController]
	[Route("products")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<ProductController> _logger;

		public ProductController(IUnitOfWork unitOfWork, ILogger<ProductController> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _unitOfWork.ProductRepository.GetAllAsync();

			return Ok(products);
		}

		[HttpGet("{id}")]

		public async Task<IActionResult> GetProductById(int id)
		{
			_logger.LogInformation($"Received request to get product with id: {id}");

			if (!ModelState.IsValid)
			{
				_logger.LogWarning($"Invalid id received {id}");
				return NotFound(ModelState);
			}

			try
			{
				var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

				_logger.LogInformation($"Product with id {id} found successfully");

				return Ok(product);
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Error getting product with id: {id}");
				return StatusCode(500, "An error occurred while getting the product");
			}
			

			
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(Product entity)
		{
			_logger.LogInformation($"Received request to add product: {entity.Name}");

			if(!ModelState.IsValid)
			{
				_logger.LogWarning($"Invalid entity received {entity.Name}");
				return BadRequest(ModelState);
			}

			try
			{
				await _unitOfWork.ProductRepository.AddAsync(entity);
				await _unitOfWork.CommitAsync();
				_logger.LogInformation($"Product added successfully: {entity.Name}");

				return Ok(entity);

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error adding product: {entity.Name}");
				return StatusCode(500, "An error occurred while adding the product");
			}
			
		}

		[HttpPut("{id}")]

		public async Task<IActionResult> UpdateProduct(int id, Product entity)
		{
			_logger.LogInformation($"Received request to update product: {entity.Name}");

			if (!ModelState.IsValid)
			{
				_logger.LogWarning($"Invalid values for {entity.Name}");
				return BadRequest(ModelState);
			}

			try
			{
				await _unitOfWork.ProductRepository.UpdateAsync(entity, id);
				await _unitOfWork.CommitAsync();

				_logger.LogInformation($"Product updated successfully: {entity.Name}");
				return Ok(entity);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Error updating product: {entity.Name}");
				return StatusCode(500, "An error occurred while updating the product");
			}
			
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			_logger.LogInformation($"Received request to delete product with id: {id}");

			try
			{
				await _unitOfWork.ProductRepository.DeleteAsync(id);
				await _unitOfWork.CommitAsync();
				_logger.LogInformation($"Product with id {id} deleted successfully");

				return Ok();
			}
			catch (Exception e)
			{
				_logger.LogError(e, $"Error deleting product with id: {id}");
				return StatusCode(500, "An error occurred while deleting the product");
			}
			
		}
		

	}
}
