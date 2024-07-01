using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model.Dto
{
    public class AuthenticationRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
