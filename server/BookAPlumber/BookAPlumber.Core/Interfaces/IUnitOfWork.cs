

namespace BookAPlumber.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ITokenRepository Tokens { get; }
        Task<int> Save();
    }
}
