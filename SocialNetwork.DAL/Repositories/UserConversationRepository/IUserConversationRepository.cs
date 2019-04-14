using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.UserConversationRepository
{
    public interface IUserConversationRepository
    {
        Task<bool> ContainsEntityWithId(int userId, int conversationId);
    }
}