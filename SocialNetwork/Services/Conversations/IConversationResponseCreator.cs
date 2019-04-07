using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Conversations;

namespace SocialNetwork.API.Services.Conversations
{
    public interface IConversationResponseCreator
    {
        IActionResult ResponseForGet(ConversationDto conversationDto);
        IActionResult ResponseForCreate(int statusCode, ConversationDtoForCreate conversationDtoForCreate);
        IActionResult ResponseForUpdate(int statusCode);
        IActionResult ResponseForDelete(int statusCode);
    }
}