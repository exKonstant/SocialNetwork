using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.BLL.Users;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Entities.Enums;
using SocialNetwork.DAL.UnitOfWork;

namespace SocialNetwork.BLL.FriendRequests
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FriendRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FriendRequestDto>> GetAllAsync()
        {
            var friendRequests = await _unitOfWork.FriendRequests.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<FriendRequestDto>>(friendRequests);
        }
        public async Task<IEnumerable<FriendRequestDto>> GetSenderRequestsAsync(int senderId)
        {
            var friendRequests = await _unitOfWork.FriendRequests.GetSenderRequestsAsync(senderId);
            return _mapper.Map<IEnumerable<FriendRequestDto>>(friendRequests);
        }

        public async Task<IEnumerable<FriendRequestDto>> GetReceiverRequestsAsync(int receiverId)
        {
            var friendRequest = await _unitOfWork.FriendRequests.GetSenderRequestsAsync(receiverId);
            return _mapper.Map<IEnumerable<FriendRequestDto>>(friendRequest);
        }

        public async Task<int> Create(FriendRequestDto friendRequestDto)
        {
            if (await _unitOfWork.FriendRequests.ContainsEntityWithId(friendRequestDto.SenderId, friendRequestDto.ReceiverId))
            {
                return -1;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.SenderId))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.ReceiverId))
            {
                return -3;
            }

            var friendRequest = _mapper.Map<FriendRequest>(friendRequestDto);
            friendRequest.FriendRequestStatus = FriendRequestStatus.Awaiting;
            await _unitOfWork.FriendRequests.AddAsync(friendRequest);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Accept(FriendRequestDto friendRequestDto)
        {
            if (!await _unitOfWork.FriendRequests.ContainsEntityWithId(friendRequestDto.SenderId, friendRequestDto.ReceiverId))
            {
                return -1;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.SenderId))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.ReceiverId))
            {
                return -3;
            }

            var friendRequest =
                await _unitOfWork.FriendRequests.GetAsync(friendRequestDto.SenderId, friendRequestDto.ReceiverId);

            friendRequest.FriendRequestStatus = FriendRequestStatus.Accepted;
            await AddFriends(friendRequestDto.SenderId, friendRequestDto.ReceiverId);
            _unitOfWork.FriendRequests.Update(friendRequest);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Decline(FriendRequestDto friendRequestDto)
        {
            if (!await _unitOfWork.FriendRequests.ContainsEntityWithId(friendRequestDto.SenderId, friendRequestDto.ReceiverId))
            {
                return -1;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.SenderId))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendRequestDto.ReceiverId))
            {
                return -3;
            }
            var friendRequest =
                await _unitOfWork.FriendRequests.GetAsync(friendRequestDto.SenderId, friendRequestDto.ReceiverId);
            friendRequest.FriendRequestStatus = FriendRequestStatus.Declined;
            _unitOfWork.FriendRequests.Update(friendRequest);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
        public async Task<int> AddFriends(int userId, int friendId)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(userId))
            {
                return -1;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendId))
            {
                return -2;
            }
            if (await _unitOfWork.UserFriends.CheckIfFriends(userId, friendId))
            {
                return -3;
            }
            if (userId == friendId)
            {
                return -4;
            }
            await _unitOfWork.UserFriends.AddAsync(new UserFriend { UserId = userId, FriendId = friendId });
            await _unitOfWork.UserFriends.AddAsync(new UserFriend {UserId = friendId, FriendId = userId});
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
        


        
    }
}
