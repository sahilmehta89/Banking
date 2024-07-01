using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services.Maps
{
    public static class MappingInitializer
    {
        public static IMapper Intialize()
        {
            // Auto Mapper Configurations  
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new AccountProfile());
                mc.AddProfile(new TransactionProfile());
                mc.AddProfile(new ContactDetailProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            return mapper;
        }
    }
}
