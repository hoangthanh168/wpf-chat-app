// MessageRepository.cs
using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatServer.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Message> GetMessagesByGroupId(int groupId)
        {
            return _dbSet.Where(m => m.GroupID == groupId)
                        .Include(m => m.Sender)
                        .Include(m => m.GroupChat)
                        .ToList();
        }

        public IEnumerable<Message> GetMessagesByUserId(int userId)
        {
            return _dbSet.Where(m => m.SenderID == userId || m.ReceiverID == userId)
                        .Include(m => m.Sender)
                        .Include(m => m.Receiver)
                        .ToList();
        }

        // Triển khai thêm các phương thức nếu cần
    }
}
