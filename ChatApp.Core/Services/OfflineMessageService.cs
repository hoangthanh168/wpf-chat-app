// OfflineMessageService.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using System.Collections.Generic;

namespace ChatServer.Services
{
    public class OfflineMessageService : IOfflineMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OfflineMessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateOfflineMessage(OfflineMessage offlineMessage)
        {
            _unitOfWork.OfflineMessages.Add(offlineMessage);
            _unitOfWork.Complete();
        }

        public void DeleteOfflineMessage(int id)
        {
            var offlineMessage = _unitOfWork.OfflineMessages.GetById(id);
            if (offlineMessage != null)
            {
                _unitOfWork.OfflineMessages.Remove(offlineMessage);
                _unitOfWork.Complete();
            }
        }

        public IEnumerable<OfflineMessage> GetAllOfflineMessages()
        {
            return _unitOfWork.OfflineMessages.GetAll();
        }

        public OfflineMessage GetOfflineMessageById(int id)
        {
            return _unitOfWork.OfflineMessages.GetById(id);
        }

        public IEnumerable<OfflineMessage> GetOfflineMessagesByUserId(int userId)
        {
            return _unitOfWork.OfflineMessages.GetOfflineMessagesByUserId(userId);
        }

        public void UpdateOfflineMessage(OfflineMessage offlineMessage)
        {
            _unitOfWork.OfflineMessages.Update(offlineMessage);
            _unitOfWork.Complete();
        }

        // Thêm các phương thức đặc thù nếu cần
    }
}
