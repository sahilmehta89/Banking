using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model.Dto
{
    public class UserViewDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public ContactDetailsDTO ContactDetails { get; set; }
        public IEnumerable<AccountViewDTO> Accounts { get; set; }
    }
}
