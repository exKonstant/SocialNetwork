using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.FriendRequests;

namespace SocialNetwork.API.Services.FriendRequests
{
    public interface IFriendRequestResponseCreator
    {
        IActionResult ResponseForAcceptOrDeclineOrCreate(int statusCode);
        IActionResult ResponseForAddFriends(int statusCode);
    }
}