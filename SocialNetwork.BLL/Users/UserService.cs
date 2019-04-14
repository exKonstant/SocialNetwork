using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.BLL.Conversations;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Entities.Enums;
using SocialNetwork.DAL.UnitOfWork;

namespace SocialNetwork.BLL.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAll().ToListAsync();            
            return _mapper.Map<IEnumerable<UserDto>>(users); 
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);           
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetFriendsAsync(int userId)
        {
            var friends = await _unitOfWork.UserFriends.GetByUser(userId).ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(friends);
        }

        public async Task<IEnumerable<ConversationDto>> GetConversationsByUserAsync(int userId)
        {
            var conversations = await _unitOfWork.Users.GetConversationsByUser(userId).ToListAsync();
            return _mapper.Map<IEnumerable<ConversationDto>>(conversations);
        }

        public async Task<int> AddAsync(UserDtoForCreate userDtoForCreate)
        {
            if (string.IsNullOrEmpty(userDtoForCreate.FirstName))
            {
                return -1;
            }
            if (string.IsNullOrEmpty(userDtoForCreate.LastName))
            {
                return -2;
            }            
            var user = _mapper.Map<User>(userDtoForCreate);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> UpdateAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FirstName))
            {
                return -1;
            }
            if (string.IsNullOrEmpty(userDto.LastName))
            {
                return -2;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(userDto.Id))
            {
                return -3;
            }
            var user = await _unitOfWork.Users.GetAsync(userDto.Id);
            _mapper.Map(userDto, user);
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return user.Id;
        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(id))
            {
                return -1;
            }            
            _unitOfWork.Users.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteFriends(int userId, int friendId)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(userId))
            {
                return -1;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(friendId))
            {
                return -2;
            }
            if (!await _unitOfWork.UserFriends.CheckIfFriends(userId, friendId))
            {
                return -3;
            }
            if (userId == friendId)
            {
                return -4;
            }
            _unitOfWork.UserFriends.Delete(userId, friendId);
            _unitOfWork.UserFriends.Delete(friendId, userId);            

            await _unitOfWork.SaveChangesAsync();
            return 1;
        }


    }
}
