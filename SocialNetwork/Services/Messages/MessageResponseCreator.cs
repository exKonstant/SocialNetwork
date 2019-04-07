using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.Services.Messages
{
    public class MessageResponseCreator : IMessageResponseCreator
    {
        private readonly IMapper _mapper;

        public MessageResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult ResponseForGet(MessageDto messageDto)
        {
            if (messageDto == null)
            {
                return new NotFoundResult();
            }
            var messageModel = _mapper.Map<MessageModel>(messageDto);
            return new OkObjectResult(messageModel);
        }
        public IActionResult ResponseForCreate(int statusCode, MessageDtoForCreate messageDtoForCreate)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid User Id.");
                case -2:
                    return new BadRequestObjectResult("Invalid Conversation Id.");
                case -3:
                    return new BadRequestObjectResult("Invalid text.");
                default:
                    return new CreatedAtRouteResult("GetMessage", new { Id = statusCode }, messageDtoForCreate);
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
                    return new BadRequestObjectResult("Invalid text.");
                case -2:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }
    }
}
