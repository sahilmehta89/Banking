using Banking.Core.Model;

namespace Banking.Core.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Get User details by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>User details</returns>
        Task<User?> GetUserAsync(string username);

        /// <summary>
        /// Get User details by Id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>User details</returns>
        Task<User?> GetUserAsync(int id);

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of users</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Add user record to the database
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Update user record
        /// </summary>
        /// <param name="user">User details</param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Delete user record
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        Task DeleteUserAsync(int id);
    }
}
