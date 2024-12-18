namespace Dataccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetItem(int id);
        IEnumerable<T> GetAll();
        void AddItem(T item);
        void UpdateItem(T item);
        void RemoveItem(int id);
    }
}
