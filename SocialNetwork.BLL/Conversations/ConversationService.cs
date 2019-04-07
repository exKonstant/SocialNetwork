using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.BLL.Messages;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.UnitOfWork;

namespace SocialNetwork.BLL.Conversations
{
    public class ConversationService : IConversationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConversationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConversationDto>> GetAllAsync()
        {
            var conversations = await _unitOfWork.Conversations.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<ConversationDto>>(conversations);
        }

        public async Task<ConversationDto> GetAsync(int id)
        {
            var conversation = await _unitOfWork.Conversations.GetAsync(id);            
            return _mapper.Map<ConversationDto>(conversation);
        }

        public async Task<int> AddAsync(ConversationDtoForCreate conversationDtoForCreate)
        {
            if (string.IsNullOrEmpty(conversationDtoForCreate.ConversationName))
            {
                return -1;
            }
            var conversation = _mapper.Map<Conversation>(conversationDtoForCreate);
            await _unitOfWork.Conversations.AddAsync(conversation);
            await _unitOfWork.SaveChangesAsync();
            return conversation.Id;
        }

        public async Task<int> UpdateAsync(ConversationDto conversationDto)
        {
            if (string.IsNullOrEmpty(conversationDto.ConversationName))
            {
                return -1;
            }
            if (!await _unitOfWork.Conversations.ContainsEntityWithId(conversationDto.Id))
            {
                return -2;
            }
            var conversation = await _unitOfWork.Conversations.GetAsync(conversationDto.Id);
            _mapper.Map(conversationDto, conversation);
            _unitOfWork.Conversations.Update(conversation);
            await _unitOfWork.SaveChangesAsync();
            return conversation.Id;

        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Conversations.ContainsEntityWithId(id))
            {
                return -1;
            }
            _unitOfWork.Conversations.Delete(id);

            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> UpdateUsersAsync(int id, int userId)
        {
            if (!await _unitOfWork.Conversations.ContainsEntityWithId(id))
            {
                return -3;
            }
            if (!await _unitOfWork.Users.ContainsEntityWithId(id))
            {
                return -4;
            }
            var conversation = await _unitOfWork.Conversations.GetAsync(id);
            conversation.UserConversations.Add(new UserConversation { UserId = userId });
            _unitOfWork.Conversations.Update(conversation);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
    }
}
