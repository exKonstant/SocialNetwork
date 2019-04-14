using System.Linq;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.ConversationRepository
{
    public interface IConversationRepository : IRepository<Conversation>
    {
        IQueryable<Message> GetMessagesByConversation(int conversationId);
        IQueryable<User> GetUsersByConversation(int conversationId);
    }
}