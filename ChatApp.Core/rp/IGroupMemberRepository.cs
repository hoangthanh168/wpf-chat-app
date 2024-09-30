// IGroupMemberRepository.cs
using ChatApp.Core.Models;

namespace ChatApp.Core.Repositories
{
    public interface IGroupMemberRepository : IGenericRepository<GroupMember>
    {
        // Thêm các phương thức đặc thù cho GroupMember nếu cần
    }
}
