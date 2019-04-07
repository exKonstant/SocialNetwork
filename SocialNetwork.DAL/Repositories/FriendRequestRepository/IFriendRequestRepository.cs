using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.FriendRequestRepository
{
    public interface IFriendRequestRepository
    {
        Task AddAsync(FriendRequest entity);
        IQueryable<FriendRequest> GetAll();
        Task<FriendRequest> GetAsync(int senderId, int receiverId);
        Task<bool> ContainsEntityWithId(int senderId, int receiverId);
        Task<IEnumerable<FriendRequest>> GetSenderRequestsAsync(int senderId);
        Task<IEnumerable<FriendRequest>> GetReceiverRequestsAsync(int receiverId);
        void Update(FriendRequest entity);
    }
}