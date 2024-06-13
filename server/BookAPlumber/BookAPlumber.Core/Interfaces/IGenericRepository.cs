

namespace BookAPlumber.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
