using ChatApp.Core.Models;
using System.Collections.Generic;

namespace ChatApp.Core.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
