using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.API.Models.Conversations;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.Services.Conversations
{
    public class ConversationResponseCreator : IConversationResponseCreator
    {
        private readonly IMapper _mapper;

        public ConversationResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult ResponseForGet(ConversationDto conversationDto)
        {
            if (conversationDto == null)
            {
                return new NotFoundResult();
            }
            var conversationModel = _mapper.Map<ConversationModel>(conversationDto);
            return new OkObjectResult(conversationModel);
        }
        public IActionResult ResponseForCreate(int statusCode, ConversationDtoForCreate conversationDtoForCreate)
        {
            switch (statusCode)
            {                
                case -1:
                    return new BadRequestObjectResult("Invalid name.");
                default:
                    return new CreatedAtRouteResult("GetConversation", new { Id = statusCode }, conversationDtoForCreate);
            }
        }

        public IActionResult ResponseForDelete(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new NotFoundResult();
                default:
                    return new NoContentResult();
            }
        }

        public IActionResult ResponseForUpdate(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid name.");
                case -2:
                    return new NotFoundResult();
                case -3:
                    return new BadRequestObjectResult("Invalid conversation id.");
                case -4:
                    return new BadRequestObjectResult("Invalid user id.");
                default:
                    return new OkResult();
            }
        }
    }
}
