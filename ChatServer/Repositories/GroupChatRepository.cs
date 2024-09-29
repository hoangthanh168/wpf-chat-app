// GroupChatRepository.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatServer.Repositories
{
    public class GroupChatRepository : GenericRepository<GroupChat>, IGroupChatRepository
    {
        public GroupChatRepository(AppDbContext context) : base(context)
        {
        }

        public GroupChat GetGroupChatWithMembers(int groupId)
        {
            return _dbSet.Include(gc => gc.GroupMembers)
                         .FirstOrDefault(gc => gc.GroupID == groupId);
        }

        public IEnumerable<GroupChat> GetGroupChatsByUserId(int userId)
        {
            return _dbSet.Where(gc => gc.GroupMembers.Any(gm => gm.UserID == userId)).ToList();
        }

        // Triển khai thêm các phương thức nếu cần
    }
}
