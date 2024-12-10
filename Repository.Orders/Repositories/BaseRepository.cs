using Microsoft.EntityFrameworkCore;

namespace Repository.Orders.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
}

public class BaseRepository<T>(OrdersDbContext context) : IBaseRepository<T> where T : class
{
    public async Task AddAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await context.Set<T>().AsNoTracking().ToListAsync();
        return result;
    }
}