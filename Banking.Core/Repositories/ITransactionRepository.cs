using Banking.Core.Model;
using Banking.Core.Model.Models;

namespace Banking.Core.Repositories
{
    public interface ITransactionRepository
    {
        /// <summary>
        /// Get User Account Transactions by account id. This will fetch transactions for their own account only
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountId">AccountId</param>
        /// <returns>List of Transactions</returns>
        Task<IEnumerable<Transaction>> GetTransactionsAsync(int userId, int accountId);

        /// <summary>
        /// Get User Account Transactions by account number. This will fetch transactions for their own account only
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="accountNo"></param>
        /// <returns>List of Transactions</returns>
        Task<IEnumerable<Transaction>> GetTransactionsByAccountNoAsync(int userId, string accountNo);

        /// <summary>
        /// Get paged transactions by account id. This will fetch transactions for their own account only
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountId">AccountId</param>
        /// <param name="pageNumber">Page No.</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>List of Transactions</returns>
        Task<PagedResult<Transaction>> GetTransactionsAsync(int userId, int accountId, int pageNumber, int pageSize);

        /// <summary>
        /// Get paged transactions by account number. This will fetch transactions for their own account only
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountId">AccountId</param>
        /// <param name="pageNumber">Page No.</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>List of Transactions</returns>
        Task<PagedResult<Transaction>> GetTransactionsByAccountNoAsync(int userId, string accountNo, int pageNumber, int pageSize);

        /// <summary>
        /// Add Transaction
        /// </summary>
        /// <param name="transaction">Transaction details</param>
        /// <returns></returns>
        Task AddTransactionAsync(Transaction transaction);
    }
}
