// MessageService.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using System.Collections.Generic;

namespace ChatServer.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateMessage(Message message)
        {
            _unitOfWork.Messages.Add(message);
            _unitOfWork.Complete();
        }

        public void DeleteMessage(int id)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message != null)
            {
                _unitOfWork.Messages.Remove(message);
                _unitOfWork.Complete();
            }
        }

        public IEnumerable<Message> GetAllMessages()
        {
            return _unitOfWork.Messages.GetAll();
        }

        public Message GetMessageById(int id)
        {
            return _unitOfWork.Messages.GetById(id);
        }

        public IEnumerable<Message> GetMessagesByGroupId(int groupId)
        {
            return _unitOfWork.Messages.GetMessagesByGroupId(groupId);
        }

        public IEnumerable<Message> GetMessagesByUserId(int userId)
        {
            return _unitOfWork.Messages.GetMessagesByUserId(userId);
        }

        public void UpdateMessage(Message message)
        {
            _unitOfWork.Messages.Update(message);
            _unitOfWork.Complete();
        }

        // Thêm các phương thức đặc thù nếu cần
    }
}
