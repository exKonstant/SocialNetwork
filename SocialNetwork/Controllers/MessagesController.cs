using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models;
using SocialNetwork.API.Models.Messages;
using SocialNetwork.API.Services.Messages;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.API.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        private readonly IMessageResponseCreator _messageResponseCreator;

        public MessagesController(IMessageService messageService, IMapper mapper, IMessageResponseCreator messageResponseCreator)
        {
            _messageService = messageService;
            _mapper = mapper; 
            _messageResponseCreator = messageResponseCreator;
        }

        /// <summary>
        /// Gets all messages.
        /// </summary>
        /// <returns>Returns all messages</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messageDtos = await _messageService.GetAllAsync();
            return _messageResponseCreator.ResponseForGetAll(messageDtos);
        }

        /// <summary>
        /// Gets message by id.
        /// </summary>
        /// <param name="id">Message id</param>
        /// <returns>Returns message by id</returns>
        /// <response code="200">If the item exists</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> Get(int id)
        {
            var messageDto = await _messageService.GetAsync(id);
            return _messageResponseCreator.ResponseForGet(messageDto);
        }

        /// <summary>
        /// Creates message.
        /// </summary>
        /// <param name="messageAddModel">Message model</param>
        /// <returns>Returns route to created message</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MessageAddModel messageAddModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var messageDto = _mapper.Map<MessageDtoForCreate>(messageAddModel);
            var statuscode = await _messageService.AddAsync(messageDto);
            return _messageResponseCreator.ResponseForCreate(statuscode, messageDto);            
        }

        /// <summary>
        /// Updates message.
        /// </summary>
        /// <param name="id">Message id</param>
        /// <param name="messageModel">Message model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] MessageUpdateModel messageModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var messageDto =
                _mapper.Map<MessageDtoForUpdate>(messageModel);
            messageDto.Id = id;
            var statuscode = await _messageService.UpdateAsync(messageDto);
            return _messageResponseCreator.ResponseForUpdate(statuscode);
        }

        /// <summary>
        /// Deletes message.
        /// </summary>
        /// <param name="id">Message id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _messageService.DeleteAsync(id);
            return _messageResponseCreator.ResponseForDelete(statuscode);
        }
    }
}
