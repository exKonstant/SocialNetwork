using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Messages;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.Services.Conversations
{
    public interface IConversationResponseCreator
    {
        IActionResult ResponseForGetUsers(IEnumerable<UserDto> userDtos);
        IActionResult ResponseForGetMessages(IEnumerable<MessageDto> messageDtos);
        IActionResult ResponseForGetAll(IEnumerable<ConversationDto> conversationDtos);
        IActionResult ResponseForGet(ConversationDto conversationDto);
        IActionResult ResponseForCreate(int statusCode, ConversationDtoForCreate conversationDtoForCreate);
        IActionResult ResponseForUpdateAndAddUsers(int statusCode);
        IActionResult ResponseForDelete(int statusCode);
    }
}