// OfflineMessageRepository.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatServer.Repositories
{
    public class OfflineMessageRepository : GenericRepository<OfflineMessage>, IOfflineMessageRepository
    {
        public OfflineMessageRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<OfflineMessage> GetOfflineMessagesByUserId(int userId)
        {
            return _dbSet.Where(om => om.UserID == userId)
                        .Include(om => om.Message)
                        .ToList();
        }

        // Triển khai thêm các phương thức nếu cần
    }
}
