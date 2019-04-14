using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.ConversationRepository
{
    public class ConversationRepository : Repository<Conversation>, IConversationRepository
    {
        private readonly DbSet<Conversation> _conversations;
        public ConversationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _conversations = DbContext.Conversations;
        }

        public override async Task AddAsync(Conversation entity)
        {
            await _conversations.AddAsync(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _conversations.AnyAsync(c => c.Id == id);
        }

        public override void Delete(int id)
        {
            var conversation = new Conversation { Id = id };
            _conversations.Remove(conversation);
        }

        public override IQueryable<Conversation> GetAll()
        {
            return _conversations
                .Include(c => c.Messages)
                .Include(c => c.UserConversations);
        }

        public override async Task<Conversation> GetAsync(int id)
        {
            return await _conversations
                .Include(c => c.Messages)
                .Include(c => c.UserConversations)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<User> GetUsersByConversation(int conversationId)
        {
            return _conversations.SelectMany(u => u.UserConversations.Where(uc => uc.ConversationId == conversationId).Select(uc => uc.User));
        }

        public IQueryable<Message> GetMessagesByConversation(int conversationId)
        {
            return _conversations.SelectMany(c => c.Messages.Where(m => m.ConversationId == conversationId));
            /*.OrderBy(m => m.DateSent)*/
        }

        public override void Update(Conversation entity)
        {
            _conversations.Update(entity);
        }
    }
}
