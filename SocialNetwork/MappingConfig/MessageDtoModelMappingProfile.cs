using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.API.Models;
using SocialNetwork.API.Models.Messages;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.MappingConfig
{
    public class MessageDtoModelMappingProfile : Profile
    {
        public MessageDtoModelMappingProfile()
        {
            CreateMap<MessageDto, MessageModel>();            
            CreateMap<MessageUpdateModel, MessageDtoForUpdate>();
            CreateMap<MessageAddModel, MessageDtoForCreate>();
        }
    }
}
