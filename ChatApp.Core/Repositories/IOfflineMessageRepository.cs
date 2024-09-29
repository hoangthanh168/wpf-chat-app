// IOfflineMessageRepository.cs
using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Repositories
{
    public interface IOfflineMessageRepository : IGenericRepository<OfflineMessage>
    {
        IEnumerable<OfflineMessage> GetOfflineMessagesByUserId(int userId);
    }
}
