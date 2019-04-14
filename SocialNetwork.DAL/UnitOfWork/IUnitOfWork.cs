using SocialNetwork.DAL.Repositories.ConversationRepository;
using SocialNetwork.DAL.Repositories.FriendRequestRepository;
using SocialNetwork.DAL.Repositories.MessageRepository;
using SocialNetwork.DAL.Repositories.UserFriendRepository;
using SocialNetwork.DAL.Repositories.UserRepository;
using System;
using System.Threading.Tasks;
using SocialNetwork.DAL.Repositories.UserConversationRepository;

namespace SocialNetwork.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        IConversationRepository Conversations { get; }
        IFriendRequestRepository FriendRequests { get; }
        IUserConversationRepository UserConversations { get; }
        IUserFriendRepository UserFriends { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}