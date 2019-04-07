using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _users = DbContext.Users;
        }
        public override async Task AddAsync(User entity)
        {
            await _users.AddAsync(entity);
        }

        public override async Task<bool> ContainsEntityWithId(int id)
        {
            return await _users.AnyAsync(u => u.Id == id);
        }

        public override void Delete(int id)
        {
            var user = new User{Id = id};
            _users.Remove(user);
        }

        public override IQueryable<User> GetAll()
        {
            return _users
                .Include(u => u.UserConversations)
                .Include(u => u.Messages);
        }

        public override async Task<User> GetAsync(int id)
        {
            return await _users
                .Include(u => u.UserConversations)
                .Include(u => u.Messages)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public override void Update(User entity)
        {
            _users.Update(entity);
        }
    }
}
