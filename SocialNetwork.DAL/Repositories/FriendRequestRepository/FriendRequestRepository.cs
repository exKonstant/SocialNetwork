using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Entities.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.FriendRequestRepository
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly DbSet<FriendRequest> _friendRequests;        
              
        public FriendRequestRepository(ApplicationDbContext applicationDbContext)
        {
            _friendRequests = applicationDbContext.FriendRequests;
        }
        public async Task AddAsync(FriendRequest entity)
        {
            await _friendRequests.AddAsync(entity);
        }

        public async Task<bool> ContainsEntityWithId(int senderId, int receiverId)
        {
            return await _friendRequests.AnyAsync(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
        }

        public async Task<FriendRequest> GetAsync(int senderId, int receiverId)
        {
            return await _friendRequests
                .Include(fr => fr.Receiver)
                .Include(fr => fr.Sender)
                .FirstOrDefaultAsync(fr => fr.ReceiverId == receiverId && fr.SenderId == senderId);
        }

        public IQueryable<FriendRequest> GetAll()
        {
            return _friendRequests
                .Include(fr => fr.Receiver)
                .Include(fr => fr.Sender);
        }        

        public async Task<IEnumerable<FriendRequest>> GetSenderRequestsAsync(int senderId)
        {
            return await _friendRequests.Where(fr => fr.SenderId == senderId && fr.FriendRequestStatus == FriendRequestStatus.Awaiting).ToListAsync();
        }
        public async Task<IEnumerable<FriendRequest>> GetReceiverRequestsAsync(int receiverId)
        {
            return await _friendRequests.Where(fr => fr.ReceiverId == receiverId && fr.FriendRequestStatus == FriendRequestStatus.Awaiting).ToListAsync();
        }

        public void Update(FriendRequest entity)
        {
            _friendRequests.Update(entity);
        }
    }
}
