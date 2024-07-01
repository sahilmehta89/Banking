using Banking.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services
{
    public class UserIdentityService: IUserIdentityService
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserIdentityService(ILogger<UserIdentityService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        /// <inheritdoc/>
        public int? GetUserId()
        {
            try
            {
                var claims = ((ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity).Claims;
                return Convert.ToInt32(claims.FirstOrDefault(x => x.Type.Contains("name")).Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling GetUserId in UserIdentityService");
            }

            return null;
        }
    }
}
