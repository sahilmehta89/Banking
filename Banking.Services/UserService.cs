using AutoMapper;
using Banking.Core;
using Banking.Core.Model;
using Banking.Core.Model.Dto;
using Banking.Core.Services;
using Banking.Utilities.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Banking.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(ILogger<UserService> logger, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration configuration) 
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public async Task<UserLoginResponseDTO?> AuthenticateAsync(AuthenticationRequestDTO authenticationRequestDTO)
        {
            var user = await _unitOfWork.User.GetUserAsync(authenticationRequestDTO.Username).ConfigureAwait(false);

            if (user == null || !HashingMethods.VerifyPasswordHash(authenticationRequestDTO.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponseDTO userLoginResponseDTO = new UserLoginResponseDTO()
            {
                Username = user.Username,
                Token = tokenHandler.WriteToken(token)
            };

            return userLoginResponseDTO;
        }

        /// <inheritdoc/>
        public async Task<UserViewDTO> GetUserDetailsAsync(int userId)
        {
            var user = await _unitOfWork.User.GetUserAsync(userId);
            if (user == null) return null;
            var userDto = _mapper.Map<UserViewDTO>(user);

            return userDto;

        }

        /// <inheritdoc/>
        public async Task<UserViewDTO> GetUserDetailsAsync(string username)
        {
            var user = await _unitOfWork.User.GetUserAsync(username);
            if (user == null) return null;
            var userDto = _mapper.Map<UserViewDTO>(user);

            return userDto;
        }

        /// <inheritdoc/>
        public async Task<UserViewDTO> RegisterAsync(UserCreateDTO userCreateDTO)
        {
            // Create password hash
            HashingMethods.CreatePasswordHash(userCreateDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = userCreateDTO.Username,
                ContactDetails = new ContactDetails()
                {
                    Email = userCreateDTO.Email,
                    PhoneNumber = userCreateDTO.Email
                },
                Accounts = new List<Account>()
                {
                    new Account()
                    {
                        AccountNumber = GenerateAccountNumber()
                    }
                }
            };

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _unitOfWork.User.AddUserAsync(user).ConfigureAwait(false);

            var userDto = _mapper.Map<UserViewDTO>(user);
            return userDto;
        }

        /// <inheritdoc/>
        public async Task UpdateUserContactDetailsAsync(int userId, ContactDetailsUpdateDTO contactDetailsUpdateDTO)
        {
            var user = await _unitOfWork.User.GetUserAsync(userId);
            if (user != null)
            {
                user.ContactDetails.Email = contactDetailsUpdateDTO.Email;
                user.ContactDetails.PhoneNumber = contactDetailsUpdateDTO.PhoneNumber;

                await _unitOfWork.User.UpdateUserAsync(user).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Generate randon Account number
        /// </summary>
        /// <returns>Account Number</returns>
        private string GenerateAccountNumber()
        {
            String startWith = "32";
            Random generator = new Random();
            String r = generator.Next(0, 999999999).ToString("D6");
            String aAccounNumber = startWith + r;

            return aAccounNumber;
        }      
    }
}
