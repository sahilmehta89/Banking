using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model.Dto
{
    public class AccountCreateDTO
    {
        [Required]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<TransactionCreateDTO> Transactions { get; set; }
    }
}
