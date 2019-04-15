using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.FriendRequestRepository
{
    public interface IFriendRequestRepository
    {
        void DeleteBySender(IEnumerable<FriendRequest> friendRequests);
        void DeleteByReceiver(IEnumerable<FriendRequest> friendRequests);
        Task<bool> ContainsEntityWithSenderId(int senderId);
        Task<bool> ContainsEntityWithReceiverId(int receiverId);
        Task AddAsync(FriendRequest entity);
        IQueryable<FriendRequest> GetAll();
        Task<FriendRequest> GetAsync(int senderId, int receiverId);
        Task<bool> ContainsEntityWithId(int senderId, int receiverId);
        Task<IEnumerable<FriendRequest>> GetSenderRequestsAsync(int senderId);
        Task<IEnumerable<FriendRequest>> GetReceiverRequestsAsync(int receiverId);
        void Update(FriendRequest entity);
    }
}