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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userDtos = await _userService.GetAllAsync();
            var userModels = _mapper.Map<IEnumerable<UserModel>>(userDtos);
            return Ok(userModels);
        }

        [HttpGet("{id}", Name = "GetUsers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var userDto = await _userService.GetAsync(id);
            return _userResponseCreator.ResponseForGet(userDto);
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetFriends(int userId)
        {
            var userDtos = await _userService.GetFriendsAsync(userId);
            var userModels = _mapper.Map<IEnumerable<UserModel>>(userDtos);
            return Ok(userModels);
        }

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

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var statuscode = await _userService.DeleteAsync(id);
            return _userResponseCreator.ResponseForDelete(statuscode);
        }

        [Authorize(Roles = "user")]
        [HttpDelete("deletefriends")]
        public async Task<IActionResult> DeleteFriends(int userId, int friendId)
        {
            var statuscode = await _userService.DeleteFriends(userId, friendId);
            return _userResponseCreator.ResponseForDeleteFriends(statuscode);
        }
    }
}
