// GroupChatService.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using System.Collections.Generic;

namespace ChatServer.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupChatService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateGroupChat(GroupChat groupChat)
        {
            _unitOfWork.GroupChats.Add(groupChat);
            _unitOfWork.Complete();
        }

        public void DeleteGroupChat(int id)
        {
            var groupChat = _unitOfWork.GroupChats.GetById(id);
            if (groupChat != null)
            {
                _unitOfWork.GroupChats.Remove(groupChat);
                _unitOfWork.Complete();
            }
        }

        public IEnumerable<GroupChat> GetAllGroupChats()
        {
            return _unitOfWork.GroupChats.GetAll();
        }

        public GroupChat GetGroupChatById(int id)
        {
            return _unitOfWork.GroupChats.GetGroupChatWithMembers(id);
        }

        public IEnumerable<GroupChat> GetGroupChatsByUserId(int userId)
        {
            return _unitOfWork.GroupChats.GetGroupChatsByUserId(userId);
        }

        public void UpdateGroupChat(GroupChat groupChat)
        {
            _unitOfWork.GroupChats.Update(groupChat);
            _unitOfWork.Complete();
        }

        // Thêm các phương thức đặc thù nếu cần
    }
}
