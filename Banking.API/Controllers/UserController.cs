using Banking.Core.Model;
using Banking.Core.Model.Dto;
using Banking.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserIdentityService _userIdentityService;

        public UserController(IUserService userService, IUserIdentityService userIdentityService)
        {
            _userService = userService;
            _userIdentityService = userIdentityService;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(UserLoginResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate(AuthenticationRequestDTO authenticationRequestDTO)
        {
            // Validate user login
            var user = await _userService.AuthenticateAsync(authenticationRequestDTO).ConfigureAwait(false);
            if (user == null) return Unauthorized();
            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(UserViewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserDetails()
        {
            // Get logged in user details
            var userId = _userIdentityService.GetUserId();
            var user = await _userService.GetUserDetailsAsync(userId.Value).ConfigureAwait(false);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserViewDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserCreateDTO userDto)
        {
            // Register or add new user record
            var existingUser = await _userService.GetUserDetailsAsync(userDto.Username).ConfigureAwait(false);

            if (existingUser != null)
            {
                return BadRequest("User already exists");
            }

            var user = await _userService.RegisterAsync(userDto).ConfigureAwait(false);
            return Ok(user);
        }

        [HttpPut("contact-details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> UpdateContactDetails(ContactDetailsUpdateDTO contactDetailsUpdateDTO)
        {
            // Update contact information
            var userId = _userIdentityService.GetUserId();
            await _userService.UpdateUserContactDetailsAsync(userId.Value, contactDetailsUpdateDTO);
            return NoContent();
        }
    }
}
