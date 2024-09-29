// GroupMemberRepository.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;

namespace ChatServer.Repositories
{
    public class GroupMemberRepository : GenericRepository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(AppDbContext context) : base(context)
        {
        }

    }
}
