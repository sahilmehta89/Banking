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
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountViewDTO>();
            CreateMap<AccountCreateDTO, Account>();
        }
    }
}
