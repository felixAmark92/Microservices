namespace InventoryService.DataAccess
{
    // Gränssnitt för Unit of Work
    public interface IUnitOfWork : IDisposable
    {
        void AddOperation(Action operation);
        Task CommitChanges();
    }
}

