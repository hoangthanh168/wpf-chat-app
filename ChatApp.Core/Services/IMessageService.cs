// IMessageService.cs
using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Services
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAllMessages();
        Message GetMessageById(int id);
        IEnumerable<Message> GetMessagesByGroupId(int groupId);
        IEnumerable<Message> GetMessagesByUserId(int userId);
        void CreateMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(int id);
        // Thêm các phương thức đặc thù nếu cần
    }
}
