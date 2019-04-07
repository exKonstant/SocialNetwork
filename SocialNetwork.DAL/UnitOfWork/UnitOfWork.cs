using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Repositories.ConversationRepository;
using SocialNetwork.DAL.Repositories.FriendRequestRepository;
using SocialNetwork.DAL.Repositories.MessageRepository;
using SocialNetwork.DAL.Repositories.UserFriendRepository;
using SocialNetwork.DAL.Repositories.UserRepository;
using System;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _disposed = false;

        private IUserRepository _userRepository;
        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }

        }

        private IMessageRepository _messageRepository;
        public IMessageRepository Messages
        {
            get
            {
                if (_messageRepository == null)
                {
                    _messageRepository = new MessageRepository(_dbContext);
                }
                return _messageRepository;
            }

        }

        private IConversationRepository _conversationRepository;     
        public IConversationRepository Conversations
        {
            get
            {
                if (_conversationRepository == null)
                {
                    _conversationRepository = new ConversationRepository(_dbContext);
                }
                return _conversationRepository;
            }

        }

        private IUserFriendRepository _userFriendRepository;
        public IUserFriendRepository UserFriends
        {
            get
            {
                if (_userFriendRepository == null)
                {
                    _userFriendRepository = new UserFriendRepository(_dbContext);
                }
                return _userFriendRepository;
            }

        }

        private IFriendRequestRepository _friendRequestRepository;
        public IFriendRequestRepository FriendRequests
        {
            get
            {
                if (_friendRequestRepository == null)
                {
                    _friendRequestRepository = new FriendRequestRepository(_dbContext);
                }
                return _friendRequestRepository;
            }

        }


        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                Users?.Dispose();
                Conversations?.Dispose();
                Messages?.Dispose();

                _dbContext?.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
