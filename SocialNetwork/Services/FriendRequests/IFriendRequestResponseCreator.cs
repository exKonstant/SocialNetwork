using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.FriendRequests;

namespace SocialNetwork.API.Services.FriendRequests
{
    public interface IFriendRequestResponseCreator
    {
        IActionResult ResponseForGet(IEnumerable<FriendRequestDtoForGet> friendRequestDtos);
        IActionResult ResponseForCreate(int statusCode);
        IActionResult ResponseForAcceptOrDecline(int statusCode);
    }
}