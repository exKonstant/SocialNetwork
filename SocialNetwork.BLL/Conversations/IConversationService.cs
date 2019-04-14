using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.BLL.Messages;
using SocialNetwork.BLL.Users;

namespace SocialNetwork.BLL.Conversations
{
    public interface IConversationService
    {
        Task<IEnumerable<ConversationDto>> GetAllAsync();
        Task<ConversationDto> GetAsync(int id);
        Task<IEnumerable<MessageDto>> GetMessagesByConversationAsync(int conversationId);
        Task<IEnumerable<UserDto>> GetUsersByConversationAsync(int conversationId);
        Task<int> AddAsync(ConversationDtoForCreate conversationDtoForCreate);
        Task<int> UpdateAsync(ConversationDto conversationDto);
        Task<int> DeleteAsync(int id);
        Task<int> AddUsersAsync(int id, int userid);
    }
}