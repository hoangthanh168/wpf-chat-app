using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class GroupChatService
    {
        private readonly AppDbContext _context;

        public GroupChatService(AppDbContext context)
        {
            _context = context;
        }

        // Create a group chat
        public void CreateGroupChat(GroupChat groupChat)
        {
            _context.GroupChats.Add(groupChat);
            _context.SaveChanges();
        }

        // Add a user to a group
        public void AddUserToGroup(int groupId, int userId)
        {
            var groupMember = new GroupMember
            {
                GroupID = groupId,
                UserID = userId,
                JoinedAt = DateTime.Now
            };

            _context.GroupMembers.Add(groupMember);
            _context.SaveChanges();
        }

        // Get group chat by ID
        public GroupChat GetGroupById(int groupId)
        {
            return _context.GroupChats.Find(groupId);
        }

        // Get all members of a group chat
        public List<User> GetGroupMembers(int groupId)
        {
            return _context.GroupMembers
                           .Where(gm => gm.GroupID == groupId)
                           .Select(gm => gm.User)
                           .ToList();
        }
    }
}
