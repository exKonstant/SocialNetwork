using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.API.Models.FriendRequests;
using SocialNetwork.API.Services.FriendRequests;
using SocialNetwork.BLL.FriendRequests;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.API.Controllers
{
    [Authorize(Roles = "user")]
    [Route("api/[controller]")]
    public class FriendRequestsController : Controller
    {
        private readonly IFriendRequestService _friendRequestService;
        private readonly IMapper _mapper; 
        private readonly IFriendRequestResponseCreator _friendRequestResponseCreator;

        public FriendRequestsController(IFriendRequestService friendRequestService, IMapper mapper, IFriendRequestResponseCreator friendRequestResponseCreator)
        {
            _friendRequestService = friendRequestService;
            _mapper = mapper;
            _friendRequestResponseCreator = friendRequestResponseCreator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var friendRequestDtos = await _friendRequestService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<FriendRequestModel>>(friendRequestDtos));
        }

        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySender(int senderId)
        {
            var friendRequestDtos = await _friendRequestService.GetSenderRequestsAsync(senderId);
            return Ok(_mapper.Map<IEnumerable<FriendRequestModel>>(friendRequestDtos));
        }

        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiver(int receiverId)
        {
            var friendRequestDtos = await _friendRequestService.GetReceiverRequestsAsync(receiverId);
            return Ok(_mapper.Map<IEnumerable<FriendRequestModel>>(friendRequestDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FriendRequestModel friendRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var friendRequestDto = _mapper.Map<FriendRequestDto>(friendRequestModel);
            var statuscode = await _friendRequestService.Create(friendRequestDto);
            return _friendRequestResponseCreator.ResponseForAddFriends(statuscode);
        }

        [HttpPut("accept")]
        public async Task<IActionResult> Accept([FromBody] FriendRequestModel friendRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var friendRequestDto =
                _mapper.Map<FriendRequestDto>(friendRequestModel);
            var statuscode = await _friendRequestService.Accept(friendRequestDto);
            return _friendRequestResponseCreator.ResponseForAcceptOrDeclineOrCreate(statuscode);
        }

        [HttpPut("declined")]
        public async Task<IActionResult> Decline([FromBody] FriendRequestModel friendRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var friendRequestDto =
                _mapper.Map<FriendRequestDto>(friendRequestModel);
            var statuscode = await _friendRequestService.Decline(friendRequestDto);
            return _friendRequestResponseCreator.ResponseForAcceptOrDeclineOrCreate(statuscode);
        }
        
    }
}
