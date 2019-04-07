using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.UnitOfWork;

namespace SocialNetwork.BLL.Messages
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetAllAsync()
        {
            var messages = await _unitOfWork.Messages.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<MessageDto> GetAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetAsync(id);
            return _mapper.Map<MessageDto>(message);
        }

        public async Task<IEnumerable<MessageDto>> GetByConversationAsync(int conversationId)
        {
            var messages = await _unitOfWork.Messages.GetByConversation(conversationId).ToListAsync();
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
        }

        public async Task<int> AddAsync(MessageDtoForCreate messageDtoForCreate)
        {
            if (!await _unitOfWork.Users.ContainsEntityWithId(messageDtoForCreate.UserId))
            {
                return -1;
            }
            if (!await _unitOfWork.Conversations.ContainsEntityWithId(messageDtoForCreate.ConversationId))
            {
                return -2;
            }
            if (string.IsNullOrEmpty(messageDtoForCreate.Text))
            {
                return -3;
            }
            var message = _mapper.Map<Message>(messageDtoForCreate);
            await _unitOfWork.Messages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
            return message.Id;
        }

        public async Task<int> UpdateAsync(MessageDtoForUpdate messageDtoForUpdate)
        {
            if (string.IsNullOrEmpty(messageDtoForUpdate.Text))
            {
                return -1;
            }
            if (!await _unitOfWork.Messages.ContainsEntityWithId(messageDtoForUpdate.Id))
            {
                return -2;
            }
            var message = await _unitOfWork.Messages.GetAsync(messageDtoForUpdate.Id);
            _mapper.Map(messageDtoForUpdate, message);
            _unitOfWork.Messages.Update(message);
            await _unitOfWork.SaveChangesAsync();
            return message.Id;

        }

        public async Task<int> DeleteAsync(int id)
        {
            if (!await _unitOfWork.Messages.ContainsEntityWithId(id))
            {
                return -1;
            }
            _unitOfWork.Messages.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
    }
}
