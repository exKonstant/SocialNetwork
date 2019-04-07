using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.Services.Messages
{
    public interface IMessageResponseCreator
    {
        IActionResult ResponseForGet(MessageDto messageDto);
        IActionResult ResponseForCreate(int statusCode, MessageDtoForCreate messageDtoForCreate);
        IActionResult ResponseForUpdate(int statusCode);
        IActionResult ResponseForDelete(int statusCode);
    }
}