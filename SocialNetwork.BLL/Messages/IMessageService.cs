using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Messages
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDto>> GetAllAsync();
        Task<MessageDto> GetAsync(int id);        
        Task<int> AddAsync(MessageDtoForCreate messageDtoForCreate);
        Task<int> UpdateAsync(MessageDtoForUpdate messageDtoForUpdate);
        Task<int> DeleteAsync(int id);
    }
}
