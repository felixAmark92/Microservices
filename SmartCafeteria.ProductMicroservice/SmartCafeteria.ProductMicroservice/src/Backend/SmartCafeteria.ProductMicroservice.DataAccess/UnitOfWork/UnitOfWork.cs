using SmartCafeteria.ProductMicroservice.DataAccess.Repositories;
using SmartCafeteria.ProductMicroservice.DataAccess.RepositoryInterfaces;

namespace SmartCafeteria.ProductMicroservice.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	public IProductRepository _productRepository { get; private set; }

	private readonly ProductServiceDbContext _context;

	public UnitOfWork(ProductServiceDbContext dbContext, IProductRepository productRepository)
	{
		_context = dbContext;
		_productRepository = productRepository;
	}

	public IProductRepository ProductRepository
	{
		get
		{
			if (_productRepository == null)
			{
				_productRepository = new ProductRepository(_context);
			}

			return _productRepository;
		}
	}
	public async Task CommitAsync()
	{
		await _context.SaveChangesAsync();
	}
	public void Dispose()
	{
		_context.DisposeAsync();
	}
}