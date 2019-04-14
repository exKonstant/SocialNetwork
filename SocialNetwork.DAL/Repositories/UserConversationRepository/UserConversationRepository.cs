using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.Repositories.UserConversationRepository
{
    public class UserConversationRepository : IUserConversationRepository
    {
        private readonly DbSet<UserConversation> _userConversations;
        public UserConversationRepository(ApplicationDbContext applicationDbContext)
        {
            _userConversations = applicationDbContext.UserConversations;
        }
        public async Task<bool> ContainsEntityWithId(int userId, int conversationId)
        {
            return await _userConversations.AnyAsync(uc => uc.UserId == userId && uc.ConversationId == conversationId);
        }
    }
}
