using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.BLL.Messages;

namespace SocialNetwork.BLL.Conversations
{
    public interface IConversationService
    {
        Task<IEnumerable<ConversationDto>> GetAllAsync();
        Task<ConversationDto> GetAsync(int id);
        Task<int> AddAsync(ConversationDtoForCreate conversationDtoForCreate);
        Task<int> UpdateAsync(ConversationDto conversationDto);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateUsersAsync(int id, int userid);
    }
}