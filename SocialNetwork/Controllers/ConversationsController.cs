using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Services.Conversations;
using SocialNetwork.BLL.Conversations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SocialNetwork.API.Models.Conversations;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    public class ConversationsController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;
        private readonly IConversationResponseCreator _conversationResponseCreator;

        public ConversationsController(IConversationService conversationService, IMapper mapper, IConversationResponseCreator conversationResponseCreator)
        {
            _conversationService = conversationService;
            _mapper = mapper;
            _conversationResponseCreator = conversationResponseCreator;            
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conversationDtos = await _conversationService.GetAllAsync();
            var conversationModels = _mapper.Map<IEnumerable<ConversationModel>>(conversationDtos);
            return Ok(conversationModels);
        }

        [Authorize(Roles = "user")]
        [HttpGet("{id}", Name = "GetConversation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var conversationDto = await _conversationService.GetAsync(id);
            return _conversationResponseCreator.ResponseForGet(conversationDto);
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ConversationAddOrUpdateModel conversationAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conversationDto = _mapper.Map<ConversationDtoForCreate>(conversationAddOrUpdateModel);
            var statuscode = await _conversationService.AddAsync(conversationDto);
            return _conversationResponseCreator.ResponseForCreate(statuscode, conversationDto);
        }

        [Authorize(Roles = "user")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] ConversationAddOrUpdateModel conversationAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conversationDto =
                _mapper.Map<ConversationDto>(conversationAddOrUpdateModel);
            conversationDto.Id = id;
            var statuscode = await _conversationService.UpdateAsync(conversationDto);
            return _conversationResponseCreator.ResponseForUpdate(statuscode);
        }

        [Authorize(Roles = "user")]
        [HttpPut("{id}/users/{userId}")]
        public async Task<IActionResult> UpdateUsers(int id, int userId)
        {
            var statuscode = await _conversationService.UpdateUsersAsync(id, userId);
            return _conversationResponseCreator.ResponseForUpdate(statuscode);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _conversationService.DeleteAsync(id);
            return _conversationResponseCreator.ResponseForDelete(statuscode);
        }
    }
}
