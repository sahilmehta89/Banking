using Banking.Core.Model;
using Banking.Core.Model.Models;
using Banking.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.PostgreSQL.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankingDbContext context)
            : base(context)
        {

        }

        private BankingDbContext _context => Context as BankingDbContext;

        /// <inheritdoc/>
        public async Task<IEnumerable<Transaction>> GetTransactionsAsync(int userId, int accountId)
        {
            return await _context.Transactions.Where(t => t.SourceAccountId == accountId && t.SourceAccount.UserId == userId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountNoAsync(int userId, string accountNo)
        {
            return await _context.Transactions.Where(t => t.SourceAccount.AccountNumber == accountNo && t.SourceAccount.UserId == userId).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Transaction>> GetTransactionsAsync(int userId, int accountId, int pageNumber, int pageSize)
        {
            var query = _context.Transactions.Where(t => t.SourceAccountId == accountId && t.SourceAccount.UserId == userId);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return new PagedResult<Transaction>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        /// <inheritdoc/>
        public async Task<PagedResult<Transaction>> GetTransactionsByAccountNoAsync(int userId, string accountNo, int pageNumber, int pageSize)
        {
            var query = _context.Transactions.Where(t => t.SourceAccount.AccountNumber == accountNo && t.SourceAccount.UserId == userId);
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync().ConfigureAwait(false);

            return new PagedResult<Transaction>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        /// <inheritdoc/>
        public async Task AddTransactionAsync(Transaction transaction)
        {
            await AddAsync(transaction).ConfigureAwait(false);
        }
    }
}
