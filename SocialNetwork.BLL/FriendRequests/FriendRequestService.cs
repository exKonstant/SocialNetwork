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

        public async Task<IEnumerable<FriendRequestDtoForGet>> GetAllAsync()
        {
            var friendRequests = await _unitOfWork.FriendRequests.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<FriendRequestDtoForGet>>(friendRequests);
        }
        public async Task<IEnumerable<FriendRequestDtoForGet>> GetSenderRequestsAsync(int senderId)
        {
            var friendRequests = await _unitOfWork.FriendRequests.GetSenderRequestsAsync(senderId);
            return _mapper.Map<IEnumerable<FriendRequestDtoForGet>>(friendRequests);
        }

        public async Task<IEnumerable<FriendRequestDtoForGet>> GetReceiverRequestsAsync(int receiverId)
        {
            var friendRequest = await _unitOfWork.FriendRequests.GetReceiverRequestsAsync(receiverId);
            return _mapper.Map<IEnumerable<FriendRequestDtoForGet>>(friendRequest);
        }

        public async Task<int> Create(FriendRequestDto friendRequestDto)
        {
            var friendRequestTest = await
                _unitOfWork.FriendRequests.GetAsync(friendRequestDto.SenderId, friendRequestDto.ReceiverId);

            if (await _unitOfWork.FriendRequests.ContainsEntityWithId(friendRequestDto.SenderId, friendRequestDto.ReceiverId) && friendRequestTest.FriendRequestStatus == FriendRequestStatus.Awaiting)
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
            if (await _unitOfWork.UserFriends.CheckIfFriends(friendRequestDto.SenderId, friendRequestDto.ReceiverId))
            {
                return -4;
            }
            if (friendRequestDto.SenderId == friendRequestDto.ReceiverId)
            {
                return -5;
            }

            if (await _unitOfWork.FriendRequests.ContainsEntityWithId(friendRequestDto.SenderId, friendRequestDto.ReceiverId) && (friendRequestTest.FriendRequestStatus == FriendRequestStatus.Accepted || friendRequestTest.FriendRequestStatus == FriendRequestStatus.Declined))
            {
                friendRequestTest.FriendRequestStatus = FriendRequestStatus.Awaiting;
                _unitOfWork.FriendRequests.Update(friendRequestTest);
            }
            else
            {
                var friendRequest = _mapper.Map<FriendRequest>(friendRequestDto);
                friendRequest.FriendRequestStatus = FriendRequestStatus.Awaiting;
                await _unitOfWork.FriendRequests.AddAsync(friendRequest);
            }

            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Accept(int senderId, int receiverId)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(senderId))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(receiverId))
            {
                return -3;
            }
            if (!await _unitOfWork.FriendRequests.ContainsEntityWithId(senderId, receiverId))
            {
                return -1;
            }


            var friendRequest =
                await _unitOfWork.FriendRequests.GetAsync(senderId, receiverId);

            if (friendRequest.FriendRequestStatus == FriendRequestStatus.Accepted)
            {
                return -5;
            }
            friendRequest.FriendRequestStatus = FriendRequestStatus.Accepted;
            await AddFriends(senderId, receiverId);
            _unitOfWork.FriendRequests.Update(friendRequest);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> Decline(int senderId, int receiverId)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(senderId))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(receiverId))
            {
                return -3;
            }
            if (!await _unitOfWork.FriendRequests.ContainsEntityWithId(senderId, receiverId))
            {
                return -1;
            }

            if (await _unitOfWork.UserFriends.CheckIfFriends(senderId, receiverId))
            {
                _unitOfWork.UserFriends.Delete(senderId, receiverId);
                _unitOfWork.UserFriends.Delete(receiverId, senderId);
            }
            
            var friendRequest =
                await _unitOfWork.FriendRequests.GetAsync(senderId, receiverId);

            if (friendRequest.FriendRequestStatus == FriendRequestStatus.Declined)
            {
                return -6;
            }

            friendRequest.FriendRequestStatus = FriendRequestStatus.Declined;
            _unitOfWork.FriendRequests.Update(friendRequest);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
        public async Task AddFriends(int userId, int friendId)
        {
            await _unitOfWork.UserFriends.AddAsync(new UserFriend { UserId = userId, FriendId = friendId });
            await _unitOfWork.UserFriends.AddAsync(new UserFriend { UserId = friendId, FriendId = userId });
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
