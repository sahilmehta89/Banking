using Banking.Core.Model.Dto;
using Banking.Core.Services;
using Banking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserIdentityService _userIdentityService;

        public TransactionController(ITransactionService transactionService, IUserIdentityService userIdentityService)
        {
            _transactionService = transactionService;
            _userIdentityService = userIdentityService;
        }

        [HttpGet("{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetTransactions(int accountId)
        {
            // Get Transactions for this login user by accountid
            var userId = _userIdentityService.GetUserId();
            var transactions = await _transactionService.GetTransactionsAsync(userId.Value, accountId).ConfigureAwait(false);
            return Ok(transactions);
        }

        [HttpGet("AccountNumber/{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionsByAccountNoAsync(string accountNo)
        {
            // Get Transactions for this login user by account no
            var userId = _userIdentityService.GetUserId();
            var transactions = await _transactionService.GetTransactionsByAccountNoAsync(userId.Value, accountNo).ConfigureAwait(false);
            return Ok(transactions);
        }

        [HttpGet("{accountId}/{pageSize}/{pageNumber}")]
        [Authorize]
        public async Task<IActionResult> GetTransactions(int accountId, int pageSize = 10, int pageNumber = 1)
        {
            // Get paginated transaction results by account Id
            var userId = _userIdentityService.GetUserId();
            var transactions = await _transactionService.GetTransactionsAsync(userId.Value, accountId, pageNumber, pageSize).ConfigureAwait(false);
            return Ok(transactions);
        }

        [HttpGet("AccountNumber/{accountNo}/{pageSize}/{pageNumber}")]
        [Authorize]
        public async Task<IActionResult> GetTransactionsByAccountNo(string accountNo, int pageSize = 10, int pageNumber = 1)
        {
            // Get paginated transaction results by account number
            var userId = _userIdentityService.GetUserId();
            var transactions = await _transactionService.GetTransactionsByAccountNoAsync(userId.Value, accountNo, pageNumber, pageSize).ConfigureAwait(false);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionCreateDTO transactionDto)
        {
            // Add transaction to the user account
            var userId = _userIdentityService.GetUserId();
            await _transactionService.AddTransactionAsync(userId.Value, transactionDto).ConfigureAwait(false);
            return Ok();
        }
    }
}
