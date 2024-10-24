using ChatApp.Core.Models;
using ChatApp.Core.Services;
using ChatApp.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ChatApp.Core
{
    public class UserCreation
    {
        private readonly UserService _userService;

        public UserCreation(UserService userService)
        {
            _userService = userService;
        }

        public void CreateUser(string username, string password)
        {
            var existingUser = _userService.GetUserByUsername(username);
            if (existingUser != null)
            {
                return;
            }

            string passwordHash = Security.Encrypt(password);

            var newUser = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
            };

            _userService.CreateUser(newUser);
        }

        public static void SeedFirstUser(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                var userCreation = new UserCreation(userService);
                userCreation.CreateUser("server123", "password123");
            }
        }
    }

}
