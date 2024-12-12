namespace WebShop.Repositories;

public interface IRepository<TModel, in TId> where TModel : class, IModel<TId>
{
      void Add(TModel model);
      IEnumerable<TModel> GetAll();
      TModel? GetById(TId id);
      void Delete(TId id);
      void Update(TModel model);
}