using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.FriendRequests;

namespace SocialNetwork.API.Services.FriendRequests
{
    public class FriendRequestResponseCreator : IFriendRequestResponseCreator
    {
        public IActionResult ResponseForAcceptOrDeclineOrCreate(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Request with these senderId and receiverId doesn't exist.");
                case -2:
                    return new BadRequestObjectResult("Invalid sender id.");
                case -3:
                    return new BadRequestObjectResult("Invalid receiver id.");                
                default:
                    return new OkResult();
            }
        }
        public IActionResult ResponseForAddFriends(int statusCode)
        {
            switch (statusCode)
            {                
                case -1:
                    return new BadRequestObjectResult("Invalid user id.");
                case -2:
                    return new BadRequestObjectResult("Invalid friend id.");
                case -3:
                    return new BadRequestObjectResult("Requested users are already friends.");
                case -4:
                    return new BadRequestObjectResult("Invalid user and friend id.");
                default:
                    return new OkResult();
            }
        }

    }
}
