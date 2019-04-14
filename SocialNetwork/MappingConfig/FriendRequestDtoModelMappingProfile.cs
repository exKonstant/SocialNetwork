using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.API.Models.FriendRequests;
using SocialNetwork.BLL.FriendRequests;

namespace SocialNetwork.API.MappingConfig
{
    public class FriendRequestDtoModelMappingProfile : Profile
    {
        public FriendRequestDtoModelMappingProfile()
        {
            CreateMap<FriendRequestDto, FriendRequestModel>();
            CreateMap<FriendRequestModel, FriendRequestDto>();
            CreateMap<FriendRequestDtoForGet, FriendRequestGetModel>();
            CreateMap<FriendRequestGetModel, FriendRequestDtoForGet>();
        }
    }
}
