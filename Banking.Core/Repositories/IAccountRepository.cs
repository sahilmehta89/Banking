using Banking.Core.Model;

namespace Banking.Core.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Get all user accounts
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>List of accounts</returns>
        Task<IEnumerable<Account>> GetAccountsAsync(int userId);

        /// <summary>
        /// Get particular user account by providing account id. This will fetch their own account only
        /// </summary>
        /// <param name="userId">Userid</param>
        /// <param name="accountId">AccountId</param>
        /// <returns>Account details</returns>
        Task<Account> GetAccountByIdAsync(int userId, int accountId);

        /// <summary>
        /// Get particuar account by account id
        /// </summary>
        /// <param name="accountId">AccountId</param>
        /// <returns>Account details</returns>
        Task<Account?> GetAccountByIdAsync(int accountId);

        /// <summary>
        /// Get particuar user account by account number. This will fetch their own account only 
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountNumber">Account No</param>
        /// <returns>Account details</returns>
        Task<Account?> GetAccountByAccountNumberAsync(int userId, string accountNumber);

        /// <summary>
        /// Update account information
        /// </summary>
        /// <param name="account">Account details</param>
        /// <returns></returns>
        Task UpdateAccountAsync(Account account);
    }
}
