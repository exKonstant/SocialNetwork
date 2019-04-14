using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.Conversations;
using SocialNetwork.API.Models.Users;
using SocialNetwork.API.Services.Conversations;
using SocialNetwork.API.Services.Users;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUserResponseCreator _userResponseCreator;

        public UsersController(IUserService userService, IMapper mapper, IUserResponseCreator userResponseCreator)
        {
            _userService = userService;
            _mapper = mapper;
            _userResponseCreator = userResponseCreator;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>Returns all users</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userDtos = await _userService.GetAllAsync();
            return _userResponseCreator.ResponseForGetAllAndGetFriends(userDtos);
        }

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Returns user by id</returns>
        /// <response code="200">If the item exists</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var userDto = await _userService.GetAsync(id);
            return _userResponseCreator.ResponseForGet(userDto);
        }

        /// <summary>
        /// Gets friends by user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns friends by user id</returns>
        /// <response code="200">If items exist</response>
        /// <response code="404">If items are not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("{userId}/friends")]
        public async Task<IActionResult> GetFriends(int userId)
        {
            var userDtos = await _userService.GetFriendsAsync(userId);
            return _userResponseCreator.ResponseForGetAllAndGetFriends(userDtos);
        }

        /// <summary>
        /// Gets conversations by user id.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Returns conversations by user id</returns>
        /// <response code="200">If items exist</response>
        /// <response code="404">If items are not found</response>
        [Authorize(Roles = "user")]
        [HttpGet("{userId}/conversations")]
        public async Task<IActionResult> GetConversations(int userId)
        {
            var conversationDtos = await _userService.GetConversationsByUserAsync(userId);
            return _userResponseCreator.ResponseForGetConversations(conversationDtos);
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        /// <param name="userAddOrUpdateModel">User model</param>
        /// <returns>Returns route to created user</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] UserAddOrUpdateModel userAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<UserDtoForCreate>(userAddOrUpdateModel);
            var statuscode = await _userService.AddAsync(userDto);
            return _userResponseCreator.ResponseForCreate(statuscode, userDto);
        }

        /// <summary>
        /// Updates user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="userAddOrUpdateModel">User model</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [Authorize(Roles = "user")]
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UserAddOrUpdateModel userAddOrUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto =
                _mapper.Map<UserDto>(userAddOrUpdateModel);
            userDto.Id = id;
            var statuscode = await _userService.UpdateAsync(userDto);
            return _userResponseCreator.ResponseForUpdate(statuscode);

        }

        /// <summary>
        /// Deletes user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _userService.DeleteAsync(id);
            return _userResponseCreator.ResponseForDelete(statuscode);
        }

        /// <summary>
        /// Deletes user's friend.
        /// </summary>
        /// <param name="userId">User id</param>  
        /// <param name="friendId">Friend Id</param>     
        /// <response code="204">If the item deleted</response>
        /// <response code="404">If the item not found</response>
        [Authorize(Roles = "user")]
        [HttpDelete("deletefriends")]
        public async Task<IActionResult> DeleteFriends(int userId, int friendId)
        {
            var statuscode = await _userService.DeleteFriends(userId, friendId);
            return _userResponseCreator.ResponseForDeleteFriends(statuscode);
        }
    }
}
