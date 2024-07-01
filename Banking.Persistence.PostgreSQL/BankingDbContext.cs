using Banking.Core.Model;
using Banking.Persistence.PostgreSQL.Configurations;
using Banking.Utilities.Common;
using Microsoft.EntityFrameworkCore;

namespace Banking.Persistence.PostgreSQL
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new ContactDetailsConfiguration())
                        .ApplyConfiguration(new AccountConfiguration())
                        .ApplyConfiguration(new TransactionConfiguration());


            PopulateSeedData(modelBuilder);
        }

        private void PopulateSeedData(ModelBuilder modelBuilder)
        {
            HashingMethods.CreatePasswordHash("Test123", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "JohnSmith236",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,               
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 2,
                Username = "Ben237",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            });

            modelBuilder.Entity<ContactDetails>().HasData(new ContactDetails
            {
                Id = 1,
                UserId = 1,
                Email = "JohnSmith@gmail.com",
                PhoneNumber = "123456"
            });

            modelBuilder.Entity<ContactDetails>().HasData(new ContactDetails
            {
                Id = 2,
                UserId = 2,
                Email = "Ben237@gmail.com",
                PhoneNumber = "123456888"
            });

            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 1,
                UserId = 1,
                AccountNumber = "D5625316",
                Balance = 1000,
            });

            modelBuilder.Entity<Account>().HasData(new Account
            {
                Id = 2,
                UserId = 2,
                AccountNumber = "D5633319",
                Balance = 2000,
            });

            modelBuilder.Entity<Transaction>().HasData(new Transaction
            {
                Id = 1,
                Date = DateTime.UtcNow,
                Amount = 50,
                SourceAccountId = 1,
                DestinationAccountId = 2,
                Description = "Test",
                Type = Core.Model.Enum.TransactionType.Debit
            });

            modelBuilder.Entity<Transaction>().HasData(new Transaction
            {
                Id = 2,
                Date = DateTime.UtcNow,
                Amount = 60,
                SourceAccountId = 2,
                DestinationAccountId = 1,
                Description = "Test",
                Type = Core.Model.Enum.TransactionType.Credit
            });
        }
    }
}
