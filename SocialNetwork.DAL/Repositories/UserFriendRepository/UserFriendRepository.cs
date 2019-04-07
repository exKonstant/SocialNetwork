using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.UserFriendRepository
{
    public class UserFriendRepository : IUserFriendRepository
    {
        private readonly DbSet<UserFriend> _userfriends;

        public UserFriendRepository(ApplicationDbContext applicationDbContext)
        {
            _userfriends = applicationDbContext.UserFriends;
        }

        public async Task AddAsync(UserFriend entity)
        {
            await _userfriends.AddAsync(entity);
        }

        public IQueryable<User> GetByUser(int userId)
        {
            return _userfriends.Where(uf => uf.UserId == userId).Select(uf => uf.Friend);
        }

        public void Delete(int userId, int friendId)
        {
            var friend = new UserFriend { UserId = userId, FriendId = friendId };
            _userfriends.Remove(friend);
        }


        public IQueryable<UserFriend> GetAll()
        {
            return _userfriends
                .Include(uf => uf.User)
                .Include(uf => uf.Friend);
        }

        public async Task<bool> CheckIfFriends(int requestUserId, int targetUserId)
        {            
            return await _userfriends.AnyAsync(uf =>
                (uf.UserId == requestUserId && uf.FriendId == targetUserId) ||
                (uf.UserId == targetUserId && uf.FriendId == requestUserId));
        }
    }
}
