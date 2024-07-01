using AutoMapper;
using Banking.Core.Model.Dto;
using Banking.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services.Maps
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionViewDTO>();
            CreateMap<TransactionCreateDTO, Transaction>();
        }
    }
}
