using BookAPlumber.Core.Interfaces;
using BookAPlumber.Infrastructure.Data;

namespace BookAPlumber.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookAPlumberDbContext dbContext;
        public ITokenRepository Tokens { get; private set; }

        public UnitOfWork(BookAPlumberDbContext dbContext, ITokenRepository tokenRepository)
        {
            this.dbContext = dbContext;
            Tokens = tokenRepository;
        }

        public async Task<int> Save()
        {
            return await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
    }
}
