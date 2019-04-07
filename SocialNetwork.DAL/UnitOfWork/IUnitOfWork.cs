using SocialNetwork.DAL.Repositories.ConversationRepository;
using SocialNetwork.DAL.Repositories.FriendRequestRepository;
using SocialNetwork.DAL.Repositories.MessageRepository;
using SocialNetwork.DAL.Repositories.UserFriendRepository;
using SocialNetwork.DAL.Repositories.UserRepository;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IMessageRepository Messages { get; }
        IConversationRepository Conversations { get; }
        IFriendRequestRepository FriendRequests { get; }
        IUserFriendRepository UserFriends { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}