using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model.Dto
{
    public class UserLoginResponseDTO
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
