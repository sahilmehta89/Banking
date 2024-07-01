using Banking.Core.Model;
using Banking.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.PostgreSQL.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(BankingDbContext context)
            : base(context)
        {

        }

        private BankingDbContext _context => Context as BankingDbContext;

        /// <inheritdoc/>
        public async Task<IEnumerable<Account>> GetAccountsAsync(int userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
            return await _context.Accounts.Where(a => a.Id == accountId).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Account?> GetAccountByIdAsync(int userId, int accountId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId && a.Id == accountId).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Account?> GetAccountByAccountNumberAsync(int userId, string accountNumber)
        {
            return await _context.Accounts.Where(a => a.UserId == userId && a.AccountNumber == accountNumber).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task UpdateAccountAsync(Account account)
        {
            await UpdateAsync(account).ConfigureAwait(false);
        }
    }
}
