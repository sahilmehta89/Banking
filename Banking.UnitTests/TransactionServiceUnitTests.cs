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
    public class TransactionServiceUnitTests : IDisposable
    {
        protected readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        private Mock<IConfiguration> _configuration;
        private Mock<ILogger<TransactionService>> _logger;
        private UnitOfWork _unitOfWork;
        private TransactionService _transactionService;

        public TransactionServiceUnitTests()
        {
            var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new BankingDbContext(options);
            _context.Database.EnsureCreated();
            _mapper = MappingInitializer.Intialize();

            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("eWE+F4V5H5iYA5NpEvFIBdfJR0taAsQP1CNE+StrqNI=");
            _logger = new Mock<ILogger<TransactionService>>();
            _unitOfWork = new UnitOfWork(_context);
            _transactionService = new TransactionService(_logger.Object, _mapper, _unitOfWork);
        }

        [Fact]
        public async Task GetTransactionsAsync_ByAccountId_ShouldReturnTransactions()
        {
            var result = await _transactionService.GetTransactionsAsync(1, 1);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetTransactionsAsync_ByAccountId_ShouldNotReturnTransactions()
        {
            var result = await _transactionService.GetTransactionsAsync(1, 2);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTransactionsAsync_ByAccountNo_ShouldReturnTransactions()
        {
            var result = await _transactionService.GetTransactionsByAccountNoAsync(1, "D5625316");
            Assert.Single(result);
        }

        [Fact]
        public async Task GetTransactionsAsync_ByAccountId_ShouldReturnPagedTransactions()
        {
            var result = await _transactionService.GetTransactionsAsync(1, 1, 1, 1);

            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetTransactionsAsync_ByAccountNo_ShouldReturnPagedTransactions()
        {
            var result = await _transactionService.GetTransactionsByAccountNoAsync(1, "D5625316", 1, 1);

            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task AddTransactionAsync_ShouldAddTransaction()
        {
            await _transactionService.AddTransactionAsync(1, new Core.Model.Dto.TransactionCreateDTO()
            {
                AccountId = 1,
                Amount = 200,
                Description = "TestTransaction",
                Type = Core.Model.Enum.TransactionType.Credit
            });

            var result = await _transactionService.GetTransactionsAsync(1, 1);
            Assert.Equal(2, result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
