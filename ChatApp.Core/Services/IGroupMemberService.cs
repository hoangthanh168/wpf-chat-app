// IGroupMemberService.cs
using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Services
{
    public interface IGroupMemberService
    {
        IEnumerable<GroupMember> GetAllGroupMembers();
        GroupMember GetGroupMember(int groupId, int userId);
        void AddGroupMember(GroupMember groupMember);
        void RemoveGroupMember(int groupId, int userId);
        // Thêm các phương thức đặc thù nếu cần
    }
}
