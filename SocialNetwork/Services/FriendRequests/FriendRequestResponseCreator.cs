using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.FriendRequests;
using SocialNetwork.API.Models.Users;
using SocialNetwork.BLL.FriendRequests;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.Services.FriendRequests
{
    public class FriendRequestResponseCreator : IFriendRequestResponseCreator
    {
        private readonly IMapper _mapper;

        public FriendRequestResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult ResponseForGet(IEnumerable<FriendRequestDtoForGet> friendRequestDtos)
        {
            var friendRequestModels = _mapper.Map<IEnumerable<FriendRequestGetModel>>(friendRequestDtos);
            return new OkObjectResult(friendRequestModels);
        }

        public IActionResult ResponseForCreate(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Request with these senderId and receiverId already exists.");
                case -2:
                    return new BadRequestObjectResult("Invalid sender id.");
                case -3:
                    return new BadRequestObjectResult("Invalid receiver id.");
                case -4:
                    return new BadRequestObjectResult("These users are already friends.");
                case -5:
                    return new BadRequestObjectResult("Invalid sender id and receiver id.");
                default:
                    return new OkResult();
            }
        }

        public IActionResult ResponseForAcceptOrDecline(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Request with these senderId and receiverId doesn't exist.");
                case -2:
                    return new BadRequestObjectResult("Invalid sender id.");
                case -3:
                    return new BadRequestObjectResult("Invalid receiver id.");
                case -4:
                    return new BadRequestObjectResult("Invalid user and friend id.");
                case -5:
                    return new BadRequestObjectResult("Request is already accepted.");
                case -6:
                    return new BadRequestObjectResult("Request is already declined.");
                default:
                    return new OkResult();
            }
        }        
    }
}
