using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Users
{
    public class UserEntityDtoMappingProfile : Profile
    {
        public UserEntityDtoMappingProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UserDtoForCreate, User>();
        }
    }
}
