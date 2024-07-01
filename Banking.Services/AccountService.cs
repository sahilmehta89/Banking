using AutoMapper;
using Banking.Core;
using Banking.Core.Model;
using Banking.Core.Model.Dto;
using Banking.Core.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Banking.Services
{
    public class AccountService: IAccountService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(ILogger<AccountService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<AccountViewDTO>> GetAccountsAsync(int userId)
        {
            var accounts = await _unitOfWork.Account.GetAccountsAsync(userId).ConfigureAwait(false);
            return _mapper.Map<List<AccountViewDTO>>(accounts);
        }

        /// <inheritdoc/>
        public async Task<AccountViewDTO?> GetAccountByIdAsync(int userId, int accountId)
        {
            var account = await _unitOfWork.Account.GetAccountByIdAsync(accountId).ConfigureAwait(false);
            if (account == null) return null;

            return _mapper.Map<AccountViewDTO>(account);
        }

        /// <inheritdoc/>
        public async Task<AccountViewDTO?> GetAccountByAccountNumberAsync(int userId, string accountNumber)
        {
            var account = await _unitOfWork.Account.GetAccountByAccountNumberAsync(userId, accountNumber).ConfigureAwait(false);
            if (account == null) return null;

            return _mapper.Map<AccountViewDTO>(account);
        }

        /// <inheritdoc/>
        public async Task TransferMoneyAsync(int userId, TransferRequestDTO transferRequestDTO)
        {
            // Handle transaction
            using (var transaction = await _unitOfWork.BeginTransaction())
            {
                try
                {
                    var fromAccount = await _unitOfWork.Account.GetAccountByIdAsync(userId, transferRequestDTO.FromAccountId).ConfigureAwait(false);
                    var toAccount = await _unitOfWork.Account.GetAccountByIdAsync(transferRequestDTO.ToAccountId).ConfigureAwait(false);

                    if (fromAccount == null || toAccount == null || fromAccount.Balance < transferRequestDTO.Amount)
                    {
                        _logger.LogDebug("Invalid transfer operation userId={0} transferRequest={1}", userId, JsonConvert.SerializeObject(transferRequestDTO));
                        throw new InvalidOperationException("Invalid transfer operation");
                    }

                    fromAccount.Balance -= transferRequestDTO.Amount;
                    toAccount.Balance += transferRequestDTO.Amount;

                    await _unitOfWork.CommitAsync().ConfigureAwait(false);

                    var transactionFrom = new Transaction
                    {
                        SourceAccountId = transferRequestDTO.FromAccountId,
                        DestinationAccountId = transferRequestDTO.ToAccountId,
                        Amount = transferRequestDTO.Amount,
                        Type = Core.Model.Enum.TransactionType.Debit,
                        Date = DateTime.UtcNow,
                        Description = "Transfer to account " + transferRequestDTO.ToAccountId,
                        IsActive = true
                    };

                    var transactionTo = new Transaction
                    {
                        SourceAccountId = transferRequestDTO.ToAccountId,
                        DestinationAccountId = transferRequestDTO.FromAccountId,
                        Amount = transferRequestDTO.Amount,
                        Type = Core.Model.Enum.TransactionType.Credit,
                        Date = DateTime.UtcNow,
                        Description = "Transfer from account " + transferRequestDTO.FromAccountId,
                        IsActive = true
                    };

                    await _unitOfWork.Transaction.AddTransactionAsync(transactionFrom).ConfigureAwait(false);
                    await _unitOfWork.Transaction.AddTransactionAsync(transactionTo).ConfigureAwait(false);

                    await transaction.CommitAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync().ConfigureAwait(false);
                    _logger.LogError(ex, "Exception occurred in TransferMoneyAsync");
                }
            }       
        }
    }
}
