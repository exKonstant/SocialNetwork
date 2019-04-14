using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.Services.Messages
{
    public interface IMessageResponseCreator
    {
        IActionResult ResponseForGetAll(IEnumerable<MessageDto> messageDtos);
        IActionResult ResponseForGet(MessageDto messageDto);
        IActionResult ResponseForCreate(int statusCode, MessageDtoForCreate messageDtoForCreate);
        IActionResult ResponseForUpdate(int statusCode);
        IActionResult ResponseForDelete(int statusCode);
    }
}