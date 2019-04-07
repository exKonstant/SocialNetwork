using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.UserFriendRepository
{
    public interface IUserFriendRepository
    {
        Task AddAsync(UserFriend entity);
        void Delete(int userId, int friendId);
        IQueryable<UserFriend> GetAll();
        IQueryable<User> GetByUser(int userId);
        Task<bool> CheckIfFriends(int requestUserId, int targetUserId);
    }
}