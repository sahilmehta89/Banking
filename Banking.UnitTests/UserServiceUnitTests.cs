using AutoMapper;
using Banking.Core;
using Banking.Persistence.PostgreSQL;
using Banking.Services;
using Banking.Services.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;

namespace Banking.UnitTests
{
    public class UserServiceUnitTests : IDisposable
    {
        protected readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        private Mock<IConfiguration> _configuration;
        private Mock<ILogger<UserService>> _logger;
        private UnitOfWork _unitOfWork;
        private UserService _userService;

        public UserServiceUnitTests()
        {
            var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _context = new BankingDbContext(options);
            _context.Database.EnsureCreated();
            _mapper = MappingInitializer.Intialize();

            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("eWE+F4V5H5iYA5NpEvFIBdfJR0taAsQP1CNE+StrqNI=");
            _logger = new Mock<ILogger<UserService>>();
            _unitOfWork = new UnitOfWork(_context);
            _userService = new UserService(_logger.Object, _mapper, _unitOfWork, _configuration.Object);
        }

        [Fact]
        public async Task GetUserDetailsAsync_ShouldReturnUser()
        {
            var user = await _userService.GetUserDetailsAsync(1);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var user = await _userService.AuthenticateAsync(new Core.Model.Dto.AuthenticationRequestDTO()
            {
                Username = "JohnSmith236",
                Password = "Test123"
            });

            Assert.NotNull(user);
            Assert.NotEmpty(user.Token);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenCredentialsAreInValid()
        {
            var user = await _userService.AuthenticateAsync(new Core.Model.Dto.AuthenticationRequestDTO()
            {
                Username = "JohnSmith236",
                Password = "Test12344"
            });

            Assert.Null(user);
        }

        [Fact]
        public async Task RegisterAsync_ShouldRegisterUser()
        {
            var user = await _userService.RegisterAsync(new Core.Model.Dto.UserCreateDTO()
            {
                Username = "TestUser1",
                Password = "TestUser1",
                Email = "abc@gmail.com",
                PhoneNumber = "123"
            });

            Assert.NotNull(user);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}