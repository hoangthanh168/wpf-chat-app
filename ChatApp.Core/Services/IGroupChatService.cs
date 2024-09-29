// IGroupChatService.cs
using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Services
{
    public interface IGroupChatService
    {
        IEnumerable<GroupChat> GetAllGroupChats();
        GroupChat GetGroupChatById(int id);
        IEnumerable<GroupChat> GetGroupChatsByUserId(int userId);
        void CreateGroupChat(GroupChat groupChat);
        void UpdateGroupChat(GroupChat groupChat);
        void DeleteGroupChat(int id);
        // Thêm các phương thức đặc thù nếu cần
    }
}
