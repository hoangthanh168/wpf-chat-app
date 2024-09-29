using ChatApp.Core.Models;
using ChatApp.Core.Repositories;
using ChatApp.Core.Services;
using System.Collections.Generic;

namespace ChatServer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers() => _unitOfWork.Users.GetAll();

        public User GetUserById(int id) => _unitOfWork.Users.GetUserWithMessages(id);

        public void CreateUser(User user)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        public void UpdateUser(User user)
        {
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }

        public void DeleteUser(int id)
        {
            var user = _unitOfWork.Users.GetById(id);
            if (user != null)
            {
                _unitOfWork.Users.Remove(user);
                _unitOfWork.Complete();
            }
        }
    }
}
