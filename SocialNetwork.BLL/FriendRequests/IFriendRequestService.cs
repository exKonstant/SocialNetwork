using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.FriendRequests
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequestDtoForGet>> GetAllAsync();
        Task<IEnumerable<FriendRequestDtoForGet>> GetSenderRequestsAsync(int senderId);
        Task<IEnumerable<FriendRequestDtoForGet>> GetReceiverRequestsAsync(int receiverId);
        Task<int> Create(FriendRequestDto friendRequestDto);
        Task<int> Accept(int senderId, int receiverId);
        Task<int> Decline(int senderId, int receiverId);
    }
}