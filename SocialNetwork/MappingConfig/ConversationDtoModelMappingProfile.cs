using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.API.Models.Conversations;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.MappingConfig
{
    public class ConversationDtoModelMappingProfile : Profile
    {
        public ConversationDtoModelMappingProfile()
        {
            CreateMap<ConversationDto, ConversationModel>();
            CreateMap<ConversationModel, ConversationDto>();
            CreateMap<ConversationAddOrUpdateModel, ConversationDtoForCreate>();
            CreateMap<ConversationAddOrUpdateModel, ConversationDto>();
        }
    }
}
