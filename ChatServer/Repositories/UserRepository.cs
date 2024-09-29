using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace ChatServer.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public User GetUserWithMessages(int userId)
        {
            return _dbSet.Include(u => u.SentMessages)
                        .Include(u => u.ReceivedMessages)
                        .FirstOrDefault(u => u.UserID == userId);
        }
    }
}
