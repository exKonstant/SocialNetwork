using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Messages
{
    class MessageEntityDtoMappingProfile : Profile
    {
        public MessageEntityDtoMappingProfile()
        {
            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();
            CreateMap<MessageDtoForUpdate, Message>();
            CreateMap<MessageDtoForCreate, Message>();
        }
    }
}
