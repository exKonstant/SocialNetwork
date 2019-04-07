using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.FriendRequests
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequestDto>> GetAllAsync();
        Task<IEnumerable<FriendRequestDto>> GetSenderRequestsAsync(int senderId);
        Task<IEnumerable<FriendRequestDto>> GetReceiverRequestsAsync(int receiverId);
        Task<int> Create(FriendRequestDto friendRequestDto);
        Task<int> Accept(FriendRequestDto friendRequestDto);
        Task<int> Decline(FriendRequestDto friendRequestDto);
        Task<int> AddFriends(int senderId, int receiverId);
    }
}