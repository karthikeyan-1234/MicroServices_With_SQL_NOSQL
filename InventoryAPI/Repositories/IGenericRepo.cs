namespace InventoryAPI.Repositories
{
    public interface IGenericRepo<T> where T:class
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Func<T,bool> predicate);
        Task SaveChangesAsync();
    }
}
