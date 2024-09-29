using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Services
{
    public interface IOfflineMessageService
    {
        IEnumerable<OfflineMessage> GetAllOfflineMessages();
        OfflineMessage GetOfflineMessageById(int id);
        IEnumerable<OfflineMessage> GetOfflineMessagesByUserId(int userId);
        void CreateOfflineMessage(OfflineMessage offlineMessage);
        void UpdateOfflineMessage(OfflineMessage offlineMessage);
        void DeleteOfflineMessage(int id);
        // Thêm các phương thức đặc thù nếu cần
    }
}
