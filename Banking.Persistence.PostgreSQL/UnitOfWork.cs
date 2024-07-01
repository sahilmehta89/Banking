using Banking.Core;
using Banking.Core.Repositories;
using Banking.Persistence.PostgreSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Persistence.PostgreSQL
{
    public class UnitOfWork : IUnitOfWork
    {
        public BankingDbContext _context { get; private set;}
        private UserRepository? _userRepository;
        private AccountRepository? _accountRepository;
        private TransactionRepository? _transactionRepository;

        public UnitOfWork(BankingDbContext context)
        {
            _context = context;
        }

        public IUserRepository User => _userRepository = _userRepository ?? new UserRepository(_context);
        public IAccountRepository Account => _accountRepository = _accountRepository ?? new AccountRepository(_context);
        public ITransactionRepository Transaction => _transactionRepository = _transactionRepository ?? new TransactionRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
