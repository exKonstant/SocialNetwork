using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.BLL.Conversations
{
    class ConversationEntityDtoMappingProfile : Profile
    {
        public ConversationEntityDtoMappingProfile()
        {
            CreateMap<ConversationDto, Conversation>();
            CreateMap<Conversation, ConversationDto>();
            CreateMap<ConversationDtoForCreate, Conversation>();
        }
    }
}
