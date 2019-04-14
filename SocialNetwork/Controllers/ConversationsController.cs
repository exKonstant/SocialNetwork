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

        /// <summary>
        /// Gets all conversations.
        /// </summary>
        /// <returns>Returns all conversations</returns>
        /// <response code="200">Always</response>
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var conversationDtos = await _conversationService.GetAllAsync();
            return _conversationResponseCreator.ResponseForGetAll(conversationDtos);
        }

        /// <summary>
        /// Gets conversation by id.
        /// </summary>
        /// <param name="id">Conversation id</param>
        /// <returns>Returns conversation by id</returns>
        /// <response code="200">If the item exists</response>
        /// <response code="404">If the item is not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("{id}", Name = "GetConversation")]
        public async Task<IActionResult> Get(int id)
        {
            var conversationDto = await _conversationService.GetAsync(id);
            return _conversationResponseCreator.ResponseForGet(conversationDto);
        }

        /// <summary>
        /// Gets messages by conversation id.
        /// </summary>
        /// <param name="conversationId">Conversation id</param>
        /// <returns>Returns messages by conversation id</returns>
        /// <response code="200">If items exist</response>
        /// <response code="404">If items are not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("conversation/{conversationId}/messages")]
        public async Task<IActionResult> GetMessages(int conversationId)
        {
            var messageDtos = await _conversationService.GetMessagesByConversationAsync(conversationId);
            return _conversationResponseCreator.ResponseForGetMessages(messageDtos);
        }

        /// <summary>
        /// Gets users by conversation id.
        /// </summary>
        /// <param name="conversationId">Conversation id</param>
        /// <returns>Returns users by conversation id</returns>
        /// <response code="200">If items exist</response>
        /// <response code="404">If items are not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("conversation/{conversationId}/users")]
        public async Task<IActionResult> GetUsers(int conversationId)
        {
            var userDtos = await _conversationService.GetUsersByConversationAsync(conversationId);
            return _conversationResponseCreator.ResponseForGetUsers(userDtos);
        }

        /// <summary>
        /// Creates conversation.
        /// </summary>
        /// <param name="conversationAddOrUpdateModel">Conversation model</param>
        /// <returns>Returns route to created conversation</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
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

        /// <summary>
        /// Updates conversation.
        /// </summary>
        /// <param name="id">Conversation id</param>
        /// <param name="conversationAddOrUpdateModel">Conversation model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
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
            return _conversationResponseCreator.ResponseForUpdateAndAddUsers(statuscode);
        }

        /// <summary>
        /// Add user into conversation.
        /// </summary>
        /// <param name="conversationId">Conversation id</param>
        /// <param name="userId">User id</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [HttpPut("{conversationId}/users/{userId}")]
        public async Task<IActionResult> AddUsers(int conversationId, int userId)
        {
            var statuscode = await _conversationService.AddUsersAsync(conversationId, userId);
            return _conversationResponseCreator.ResponseForUpdateAndAddUsers(statuscode);
        }

        /// <summary>
        /// Deletes conversation.
        /// </summary>
        /// <param name="id">Conversation id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _conversationService.DeleteAsync(id);
            return _conversationResponseCreator.ResponseForDelete(statuscode);
        }
    }
}
