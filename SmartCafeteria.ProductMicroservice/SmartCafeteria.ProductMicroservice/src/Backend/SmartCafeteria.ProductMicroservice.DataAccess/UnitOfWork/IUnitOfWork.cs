using SmartCafeteria.ProductMicroservice.DataAccess.RepositoryInterfaces;

namespace SmartCafeteria.ProductMicroservice.DataAccess.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
	IProductRepository ProductRepository { get; }
	Task CommitAsync();
}