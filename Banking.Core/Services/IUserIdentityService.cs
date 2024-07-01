using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Services
{
    public interface IUserIdentityService
    {
        /// <summary>
        /// Get UserId from the claims
        /// </summary>
        /// <returns>UserId</returns>
        int? GetUserId();
    }
}
