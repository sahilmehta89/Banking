using AutoMapper;
using Banking.Core.Model;
using Banking.Core.Model.Dto;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services.Maps
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserViewDTO, UserUpdateDTO>();
            CreateMap<UserCreateDTO, User>();
        }
    }
}
