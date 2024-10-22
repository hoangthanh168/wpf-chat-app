using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        // Create a new user
        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }


        // Find user by ID
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Find user by username
        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        // Check user login
        public User Authenticate(string username, string passwordHash)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == passwordHash);
        }

        // Update user info
        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
