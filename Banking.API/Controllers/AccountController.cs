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
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserIdentityService _userIdentityService;

        public AccountController(IAccountService accountService, IUserIdentityService userIdentityService)
        {
            _accountService = accountService;
            _userIdentityService = userIdentityService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAccounts()
        {
            // Get all accounts for a user
            var userId = _userIdentityService.GetUserId();
            var accounts = await _accountService.GetAccountsAsync(userId.Value).ConfigureAwait(false);
            return Ok(accounts);
        }

        [HttpGet("account/{accountId}")]
        [Authorize]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            // Get user account by account id
            var userId = _userIdentityService.GetUserId();
            var account = await _accountService.GetAccountByIdAsync(userId.Value, accountId).ConfigureAwait(false);
            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpGet("account/AccountNumber/{accountNo}")]
        [Authorize]
        public async Task<IActionResult> GetAccount(string accountNo)
        {
            // Get user account by account no
            var userId = _userIdentityService.GetUserId();
            var account = await _accountService.GetAccountByAccountNumberAsync(userId.Value, accountNo).ConfigureAwait(false);
            if (account == null) return NotFound();

            return Ok(account);
        }

        [HttpPost("transfer")]
        [Authorize]
        public async Task<IActionResult> TransferMoney(TransferRequestDTO transferDto)
        {
            // Transfer money from one account to other
            int userId = _userIdentityService.GetUserId().Value;
            await _accountService.TransferMoneyAsync(userId, transferDto).ConfigureAwait(false);
            return Ok();
        }
    }
}
