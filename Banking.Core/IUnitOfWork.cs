using Banking.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Banking.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IAccountRepository Account { get; }
        ITransactionRepository Transaction { get; }
        Task<int> CommitAsync();
        Task<IDbContextTransaction> BeginTransaction();
    }
}
