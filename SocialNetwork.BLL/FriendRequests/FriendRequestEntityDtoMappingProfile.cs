using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.FriendRequests
{
    public class FriendRequestEntityDtoMappingProfile : Profile
    {
        public FriendRequestEntityDtoMappingProfile()
        {
            CreateMap<FriendRequestDto, FriendRequest>();
            CreateMap<FriendRequest, FriendRequestDto>();
        }
    }
}
