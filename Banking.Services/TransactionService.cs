using AutoMapper;
using Banking.Core;
using Banking.Core.Model.Dto;
using Banking.Core.Model.Models;
using Banking.Core.Services;
using Microsoft.Extensions.Logging;

namespace Banking.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ILogger<TransactionService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TransactionViewDTO>> GetTransactionsAsync(int userId, int accountId)
        {
            var transactions = await _unitOfWork.Transaction.GetTransactionsAsync(userId, accountId).ConfigureAwait(false);
            return _mapper.Map<List<TransactionViewDTO>>(transactions);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TransactionViewDTO>> GetTransactionsByAccountNoAsync(int userId, string accountNo)
        {
            var transactions = await _unitOfWork.Transaction.GetTransactionsByAccountNoAsync(userId, accountNo).ConfigureAwait(false);
            return _mapper.Map<List<TransactionViewDTO>>(transactions);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<TransactionViewDTO>> GetTransactionsAsync(int userId, int accountId, int pageNumber, int pageSize)
        {
            var transactions = await _unitOfWork.Transaction.GetTransactionsAsync(userId, accountId, pageNumber, pageSize).ConfigureAwait(false);
            return new PagedResult<TransactionViewDTO>()
            {
                Items = _mapper.Map<List<TransactionViewDTO>>(transactions.Items),
                TotalCount = transactions.TotalCount
            };
        }

        /// <inheritdoc/>
        public async Task<PagedResult<TransactionViewDTO>> GetTransactionsByAccountNoAsync(int userId, string accountNo, int pageNumber, int pageSize)
        {
            var transactions = await _unitOfWork.Transaction.GetTransactionsByAccountNoAsync(userId, accountNo, pageNumber, pageSize).ConfigureAwait(false);
            return new PagedResult<TransactionViewDTO>()
            {
                Items = _mapper.Map<List<TransactionViewDTO>>(transactions.Items),
                TotalCount = transactions.TotalCount
            };
        }

        /// <inheritdoc/>
        public async Task AddTransactionAsync(int userId, TransactionCreateDTO transactionDto)
        {
            var fromAccount = await _unitOfWork.Account.GetAccountByIdAsync(userId, transactionDto.AccountId).ConfigureAwait(false);

            if (fromAccount == null)
            {
                throw new InvalidOperationException("Invalid transfer operation");
            }

            var transaction = _mapper.Map<Core.Model.Transaction>(transactionDto);

            transaction.SourceAccountId = transactionDto.AccountId;
            transaction.DestinationAccountId = transactionDto.AccountId;
            transaction.Type = transactionDto.Type;
            transaction.IsActive = true;
            transaction.Date = DateTime.UtcNow;

            await _unitOfWork.Transaction.AddTransactionAsync(transaction).ConfigureAwait(false);

            if (transactionDto.Type == Core.Model.Enum.TransactionType.Debit)
            {
                fromAccount.Balance -= transactionDto.Amount;
            }
            else
            {
                fromAccount.Balance += transactionDto.Amount;
            }

            await _unitOfWork.CommitAsync().ConfigureAwait(false);
        }
    }
}
