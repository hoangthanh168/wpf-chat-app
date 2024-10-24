using ChatApp.Core.Models;

namespace ChatApp.Core.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetUserWithMessages(int userId);
    }
}
