// GroupMemberService.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using System.Collections.Generic;

namespace ChatServer.Services
{
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupMemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddGroupMember(GroupMember groupMember)
        {
            _unitOfWork.GroupMembers.Add(groupMember);
            _unitOfWork.Complete();
        }

        public void RemoveGroupMember(int groupId, int userId)
        {
            var groupMember = _unitOfWork.GroupMembers.GetById(new { GroupID = groupId, UserID = userId });
            if (groupMember != null)
            {
                _unitOfWork.GroupMembers.Remove(groupMember);
                _unitOfWork.Complete();
            }
        }

        public IEnumerable<GroupMember> GetAllGroupMembers()
        {
            return _unitOfWork.GroupMembers.GetAll();
        }

        public GroupMember GetGroupMember(int groupId, int userId)
        {
            return _unitOfWork.GroupMembers.GetById(new { GroupID = groupId, UserID = userId });
        }

        // Thêm các phương thức đặc thù nếu cần
    }
}
