using SmartCafeteria.ProductMicroservice.DataAccess.Entities;

namespace ProductMicroservice.CommonInterfaces;

public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
{

	Task<TEntity> GetByIdAsync(TId id);
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task AddAsync(TEntity entity);
	Task UpdateAsync(TEntity entity, int id);
	Task DeleteAsync(TId id);

}