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

        /// <summary>
        /// Gets all friend requests.
        /// </summary>
        /// <returns>Returns all friend requests</returns>
        /// <response code="200">Always</response>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var friendRequestDtos = await _friendRequestService.GetAllAsync();
            return _friendRequestResponseCreator.ResponseForGet(friendRequestDtos);
        }

        /// <summary>
        /// Gets friend requests by sender id.
        /// </summary>
        /// <param name="senderId">Sender id</param>
        /// <returns>Returns friend requests by sender id</returns>
        /// <response code="200">If the items exist</response>
        /// <response code="404">If the items are not found</response>
        [HttpGet("sender/{senderId}")]
        public async Task<IActionResult> GetBySender(int senderId)
        {
            var friendRequestDtos = await _friendRequestService.GetSenderRequestsAsync(senderId);
            return _friendRequestResponseCreator.ResponseForGet(friendRequestDtos);
        }

        /// <summary>
        /// Gets friend requests by receiver id.
        /// </summary>
        /// <param name="receiverId">Receiver id</param>
        /// <returns>Returns friend requests by receiver id</returns>
        /// <response code="200">If the items exist</response>
        /// <response code="404">If the items are not found</response>
        [HttpGet("receiver/{receiverId}")]
        public async Task<IActionResult> GetByReceiver(int receiverId)
        {
            var friendRequestDtos = await _friendRequestService.GetReceiverRequestsAsync(receiverId);
            return _friendRequestResponseCreator.ResponseForGet(friendRequestDtos);
        }

        /// <summary>
        /// Creates friend request.
        /// </summary>
        /// <param name="friendRequestModel">Friend request model</param>
        /// <returns>Returns route to created friend request</returns>
        /// <response code="201">If the item created</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FriendRequestModel friendRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var friendRequestDto = _mapper.Map<FriendRequestDto>(friendRequestModel);
            var statuscode = await _friendRequestService.Create(friendRequestDto);
            return _friendRequestResponseCreator.ResponseForCreate(statuscode);
        }

        /// <summary>
        /// Updates friend request status to accepted.
        /// </summary>        
        /// <param name="senderId">Sender id</param>  
        /// <param name="receiverId">Receiver id</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPut("accepted")]
        public async Task<IActionResult> Accept(int senderId, int receiverId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var statuscode = await _friendRequestService.Accept(senderId, receiverId);
            return _friendRequestResponseCreator.ResponseForAcceptOrDecline(statuscode);
        }

        /// <summary>
        /// Updates friend request status to declined.
        /// </summary>        
        /// <param name="senderId">Sender id</param>  
        /// <param name="receiverId">Receiver id</param>
        /// <response code="204">If the item updated</response>
        /// <response code="400">If the model is invalid or contains invalid data</response>
        [HttpPut("declined")]
        public async Task<IActionResult> Decline(int senderId, int receiverId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var statuscode = await _friendRequestService.Decline(senderId, receiverId);
            return _friendRequestResponseCreator.ResponseForAcceptOrDecline(statuscode);
        }
        
    }
}
