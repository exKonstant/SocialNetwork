using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.API.Models.Conversations;
using SocialNetwork.API.Models.Users;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Users;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.API.Services.Users
{
    public class UserResponseCreator : IUserResponseCreator
    {
        private readonly IMapper _mapper;

        public UserResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult ResponseForGet(UserDto userDto)
        {
            if (userDto == null)
            {
                return new NotFoundResult();
            }
            var userModel = _mapper.Map<UserModel>(userDto);
            return new OkObjectResult(userModel);
        }
        public IActionResult ResponseForCreate(int statusCode, UserDtoForCreate userDtoForCreate)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid firstname.");
                case -2:
                    return new BadRequestObjectResult("Invalid lastname.");
                default:
                    return new CreatedAtRouteResult("GetUser", new { Id = statusCode }, userDtoForCreate);
            }
        }

        public IActionResult ResponseForDelete(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new NotFoundResult();
                default:
                    return new NoContentResult();
            }
        }

        public IActionResult ResponseForUpdate(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid firstname.");
                case -2:
                    return new BadRequestObjectResult("Invalid lastname.");
                case -3:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }
        public IActionResult ResponseForDeleteFriends(int statusCode)
        {
            switch (statusCode)
            {
                case -1:
                    return new BadRequestObjectResult("Invalid user id.");
                case -2:
                    return new BadRequestObjectResult("Invalid friend id.");
                case -3:
                    return new BadRequestObjectResult("Requested users aren't friends.");
                case -4:
                    return new BadRequestObjectResult("Invalid user and friend id.");
                default:
                    return new OkResult();
            }
        }
    }
}
