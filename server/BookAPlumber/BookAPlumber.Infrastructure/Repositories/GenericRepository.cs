using BookAPlumber.Core.Interfaces;
using BookAPlumber.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookAPlumber.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookAPlumberDbContext dbContext;
        public GenericRepository(BookAPlumberDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }
        public void Update(T entity) 
        {
           dbContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
    }
}
