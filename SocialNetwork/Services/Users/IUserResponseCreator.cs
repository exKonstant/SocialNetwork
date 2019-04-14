using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.Services.Users
{
    public interface IUserResponseCreator
    {
        IActionResult ResponseForGetConversations(IEnumerable<ConversationDto> conversationDto);
        IActionResult ResponseForGetAllAndGetFriends(IEnumerable<UserDto> userDtos);
        IActionResult ResponseForGet(UserDto userDto);
        IActionResult ResponseForCreate(int statusCode, UserDtoForCreate userDtoForCreate);
        IActionResult ResponseForUpdate(int statusCode);
        IActionResult ResponseForDelete(int statusCode);
        IActionResult ResponseForDeleteFriends(int statusCode);
    }
}