using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Repositories
{
    public interface IGroupChatRepository : IGenericRepository<GroupChat>
    {
        GroupChat GetGroupChatWithMembers(int groupId);
        IEnumerable<GroupChat> GetGroupChatsByUserId(int userId);
    }
}
