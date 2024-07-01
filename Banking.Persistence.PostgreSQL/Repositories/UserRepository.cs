using Banking.Core.Model;
using Banking.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.PostgreSQL.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BankingDbContext context)
            : base(context)
        {

        }

        private BankingDbContext _context => Context as BankingDbContext;

        /// <inheritdoc/>
        public async Task<User?> GetUserAsync(string username)
        {
            var user = await _context.Users.Include(u => u.ContactDetails).Include(u => u.Accounts).ThenInclude(a => a.Transactions).FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);
            return user;
        }

        /// <inheritdoc/>
        public async Task<User?> GetUserAsync(int id)
        {
            var user = await _context.Users.Include(u => u.ContactDetails).Include(u => u.Accounts).ThenInclude(a => a.Transactions).FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);
            return user;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.Include(u => u.ContactDetails).Include(u => u.Accounts).ThenInclude(a => a.Transactions).ToListAsync().ConfigureAwait(false);
            return users;
        }

        /// <inheritdoc/>
        public async Task AddUserAsync(User user)
        {
            await AddAsync(user).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task UpdateUserAsync(User user)
        {
            await UpdateAsync(user).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            User? user = await GetUserAsync(id).ConfigureAwait(false);
            if (user != null)
            {
                await Remove(user).ConfigureAwait(false);
            }
        }
    }
}
