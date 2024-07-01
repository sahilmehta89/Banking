using Banking.Core.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Core.Model.Dto
{
    public class TransactionCreateDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        [Required]
        public TransactionType Type { get; set; }
    }
}
