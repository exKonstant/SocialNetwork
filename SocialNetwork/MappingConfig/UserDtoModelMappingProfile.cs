using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.API.Models.Users;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.MappingConfig
{
    public class UserDtoModelMappingProfile : Profile
    {
        public UserDtoModelMappingProfile()
        {
            {
                CreateMap<UserDto, UserModel>();
                CreateMap<UserModel, UserDto>();
                CreateMap<UserAddOrUpdateModel, UserDtoForCreate>();
                CreateMap<UserAddOrUpdateModel, UserDto>();
            }
        }
    }
}
