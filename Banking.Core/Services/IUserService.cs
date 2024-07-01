using Banking.Core.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Services
{
    public interface IUserService
    {
        /// <summary>
        /// It will validate the user login and will return the JWT if it is valid
        /// </summary>
        /// <param name="authenticationRequestDTO">Authentication Request</param>
        /// <returns>UserLoginResponseDTO object</returns>
        Task<UserLoginResponseDTO?> AuthenticateAsync(AuthenticationRequestDTO authenticationRequestDTO);

        /// <summary>
        /// Get User Details By UserId
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <returns>UserDetails</returns>
        Task<UserViewDTO> GetUserDetailsAsync(int userId);

        /// <summary>
        /// Get User Details By Username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>UserDetails</returns>
        Task<UserViewDTO> GetUserDetailsAsync(string username);

        /// <summary>
        /// Register or Create new user for banking
        /// </summary>
        /// <param name="userCreateDTO">UserCreate details like Username, Password etc</param>
        /// <returns>UserDetails</returns>
        Task<UserViewDTO> RegisterAsync(UserCreateDTO userCreateDTO);

        /// <summary>
        /// To Update contact details
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="contactDetailsUpdateDTO">Contact Details to Update</param>
        /// <returns>Task</returns>
        Task UpdateUserContactDetailsAsync(int userId, ContactDetailsUpdateDTO contactDetailsUpdateDTO);
    }
}
