using Banking.Core.Model.Dto;
using Banking.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Services
{
    public interface ITransactionService
    {
        /// <summary>
        /// Get all Transactions for the user account by providing account id.
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="accountId">Account Id</param>
        /// <returns>List of Transactions</returns>
        Task<IEnumerable<TransactionViewDTO>> GetTransactionsAsync(int userId, int accountId);

        /// <summary>
        /// Get all Transactions for the user account by providing account number
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountNo">Account No</param>
        /// <returns>List of Transactions</returns>
        Task<IEnumerable<TransactionViewDTO>> GetTransactionsByAccountNoAsync(int userId, string accountNo);

        /// <summary>
        /// Get transactions by pagination for the particular user account by providing account id
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountId">AccountId</param>
        /// <param name="pageNumber">Page No.</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>List of Transactions</returns>
        Task<PagedResult<TransactionViewDTO>> GetTransactionsAsync(int userId, int accountId, int pageNumber, int pageSize);

        /// <summary>
        /// Get transactions by pagination for the particular user account by providing account no.
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountNo">AccountNo.</param>
        /// <param name="pageNumber">PageNo.</param>
        /// <param name="pageSize">PageSize</param>
        /// <returns>List of Transactions</returns>
        Task<PagedResult<TransactionViewDTO>> GetTransactionsByAccountNoAsync(int userId, string accountNo, int pageNumber, int pageSize);

        /// <summary>
        /// Add transaction to their own user account
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="transactionDto">Transaction Details</param>
        /// <returns></returns>
        Task AddTransactionAsync(int userId, TransactionCreateDTO transactionDto);
    }
}
