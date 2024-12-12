using Microsoft.EntityFrameworkCore;
using WebShop.UnitOfWork;

namespace InventoryService.DataAccess;

public class UnitOfWork : IUnitOfWork
{

    private readonly DbContext _session;
    private readonly List<Action> _operations;

    public UnitOfWork(InventoryDbContext client)
    {
        _session = client;
        _operations = new List<Action>();
    }

    public void AddOperation(Action operation)
    {
        _operations.Add(operation);
    }


    public async Task CommitChanges()
    {
        await using (_session)
        {
            _operations.ForEach(operation => operation.Invoke());
            await _session.SaveChangesAsync();
        }
        CleanOperations();
    }
    public void Dispose()
    {
        _session.Dispose();
        CleanOperations();
    }
    private void CleanOperations()
    {
        _operations.Clear();
    }
}