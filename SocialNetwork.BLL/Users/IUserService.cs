using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.BLL.Conversations;

namespace SocialNetwork.BLL.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<IEnumerable<UserDto>> GetFriendsAsync(int userId);
        Task<int> AddAsync(UserDtoForCreate userDtoForCreate);
        Task<int> UpdateAsync(UserDto userDto);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteFriends(int userId, int friendId);
    }
}