using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.MessageRepository
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        private readonly DbSet<Message> _messages;
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _messages = DbContext.Messages;
        }

        public override async Task AddAsync(Message entity)
        {
            await _messages.AddAsync(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _messages.AnyAsync(m => m.Id == id);
        }

        public override void Delete(int id)
        {
            var message = new Message { Id = id };
            _messages.Remove(message);
        }

        public override IQueryable<Message> GetAll()
        {
            return _messages
                .Include(m => m.Conversation)
                .Include(m => m.User);
        }

        public override async Task<Message> GetAsync(int id)
        {
            return await _messages
                .Include(m => m.Conversation)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }        

        public override void Update(Message entity)
        {
            _messages.Update(entity);
        }
    }
}
