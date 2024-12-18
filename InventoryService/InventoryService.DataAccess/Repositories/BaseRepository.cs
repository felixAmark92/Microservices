namespace InventoryService.DataAccess.Repositories;

public abstract class BaseRepository<TModel, TId> 
    : IRepository<TModel, TId> where TModel : class, IEntity<TId>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly InventoryDbContext _context;

    public BaseRepository(IUnitOfWork unitOfWork, InventoryDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }
    public void Add(TModel model)
    {
        Action operation = ()=>
            _context.Set<TModel>().Add(model);
        
        _unitOfWork.AddOperation(operation);
    }

    public IEnumerable<TModel> GetAll()
    {
        var result = _context.Set<TModel>().ToList();
        return result.ToList();
    }

    public TModel? GetById(TId id)
    {
        var result = _context.Set<TModel>().Find(id);
        return result;
    }

    public void Delete(TId id)
    {
        Action operation = () =>
        {
            var entity = _context.Set<TModel>().Find(id);
            if (entity is not null)
                _context.Set<TModel>().Remove(entity);
        };
        _unitOfWork.AddOperation(operation);
    }

    public void Update(TModel model)
    {
        Action operation = () =>
        {
            _context.Set<TModel>().Update(model);
        };
        _unitOfWork.AddOperation(operation);
    }
}