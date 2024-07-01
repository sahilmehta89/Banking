using AutoMapper;
using Banking.Persistence.PostgreSQL;
using Banking.Services.Maps;
using Banking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Banking.UnitTests
{
    public class AccountServiceUnitTests : IDisposable
    {
        protected readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        private Mock<IConfiguration> _configuration;
        private Mock<ILogger<AccountService>> _logger;
        private UnitOfWork _unitOfWork;
        private AccountService _accountService;

        public AccountServiceUnitTests()
        {
            var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new BankingDbContext(options);
            _context.Database.EnsureCreated();
            _mapper = MappingInitializer.Intialize();

            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("eWE+F4V5H5iYA5NpEvFIBdfJR0taAsQP1CNE+StrqNI=");
            _logger = new Mock<ILogger<AccountService>>();
            _unitOfWork = new UnitOfWork(_context);
            _accountService = new AccountService(_logger.Object, _mapper, _unitOfWork);
        }

        [Fact]
        public async Task GetAccountsAsync_ShouldReturnAccounts()
        {
            var result = await _accountService.GetAccountsAsync(1);
            Assert.Single(result);
            Assert.Equal(1000, result.First().Balance);
        }

        [Fact]
        public async Task GetAccountByIdAsync_ShouldReturnAccount()
        {
            var result = await _accountService.GetAccountByIdAsync(1, 1);
            Assert.NotNull(result);
            Assert.Equal(1000, result.Balance);
        }

        [Fact]
        public async Task GetAccountByAccountNumberAsync_ShouldReturnAccount()
        {
            var result = await _accountService.GetAccountByAccountNumberAsync(1, "D5625316");
            Assert.NotNull(result);
            Assert.Equal(1000, result.Balance);
        }

        [Fact]
        public async Task TransferMoneyAsync_ShouldTransferMoney()
        {
            await _accountService.TransferMoneyAsync(1, new Core.Model.Dto.TransferRequestDTO()
            {
                Amount = 200,
                FromAccountId = 1,
                ToAccountId = 2
            });

            var fromAccount = await _accountService.GetAccountByIdAsync(1, 1);
            Assert.NotNull(fromAccount);
            Assert.Equal(800, fromAccount.Balance);

            var toAccount = await _accountService.GetAccountByIdAsync(2, 2);
            Assert.NotNull(toAccount);
            Assert.Equal(2200, toAccount.Balance);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
