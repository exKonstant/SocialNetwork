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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messageDtos = await _messageService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MessageModel>>(messageDtos));
        }

        [HttpGet("{id}", Name = "GetMessage")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var messageDto = await _messageService.GetAsync(id);
            return _messageResponseCreator.ResponseForGet(messageDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetByConversation(int conversationId)
        {
            var messageDtos = await _messageService.GetByConversationAsync(conversationId);
            return Ok(_mapper.Map<IEnumerable<MessageModel>>(messageDtos));
        }

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

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _messageService.DeleteAsync(id);
            return _messageResponseCreator.ResponseForDelete(statuscode);
        }
    }
}
