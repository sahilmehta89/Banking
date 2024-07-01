using Banking.Core.Model.Dto;

namespace Banking.Core.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Get All Account Details for the user
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>List of Accounts</returns>
        Task<IEnumerable<AccountViewDTO>> GetAccountsAsync(int userId);

        /// <summary>
        /// Get paricular account details by providing account id for the user
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountId">AccountId</param>
        /// <returns>Account Details</returns>
        Task<AccountViewDTO?> GetAccountByIdAsync(int userId, int accountId);

        /// <summary>
        /// Get particular account details by providing account number for the user
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="accountNumber">Account No</param>
        /// <returns>Account Details</returns>
        Task<AccountViewDTO?> GetAccountByAccountNumberAsync(int userId, string accountNumber);

        /// <summary>
        /// To transfer money from one account to another
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="transferRequestDTO">Transfer Request Details</param>
        /// <returns></returns>
        Task TransferMoneyAsync(int userId, TransferRequestDTO transferRequestDTO);
    }
}
