using ChatApp.Core.Models;
using System;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class GroupMemberService
    {
        private readonly AppDbContext _context;

        public GroupMemberService(AppDbContext context)
        {
            _context = context;
        }

        // Add a user to a group (alternative to GroupChatService)
        public void AddMember(GroupMember groupMember)
        {
            _context.GroupMembers.Add(groupMember);
            _context.SaveChanges();
        }

        // Remove a user from a group
        public void RemoveMember(int groupId, int userId)
        {
            var groupMember = _context.GroupMembers.FirstOrDefault(gm => gm.GroupID == groupId && gm.UserID == userId);
            if (groupMember != null)
            {
                _context.GroupMembers.Remove(groupMember);
                _context.SaveChanges();
            }
        }
    }
}
