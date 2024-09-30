// IMessageRepository.cs
using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Repositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        IEnumerable<Message> GetMessagesByGroupId(int groupId);
        IEnumerable<Message> GetMessagesByUserId(int userId);
        // Thêm các phương thức đặc thù cho Message nếu cần
    }
}
