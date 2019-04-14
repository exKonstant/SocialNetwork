using System.Linq;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.UserRepository
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<Conversation> GetConversationsByUser(int userId);
    }
}