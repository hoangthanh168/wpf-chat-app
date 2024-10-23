using ChatApp.Core.Models;
using System;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class UserSessionService
    {
        private readonly AppDbContext _context;

        public UserSessionService(AppDbContext context)
        {
            _context = context;
        }

        public void CreateSession(int userId, string clientEndpoint)
        {
            // Kiểm tra nếu người dùng đã có một phiên hoạt động
            var activeSession = _context.UserSessions
                                        .FirstOrDefault(s => s.UserID == userId && s.SessionStatus == "Connected");

            if (activeSession != null)
            {
                // Kết thúc phiên cũ nếu nó tồn tại
                EndSession(userId);
            }

            // Tạo phiên mới
            var userSession = new UserSession
            {
                UserID = userId,
                ClientEndpoint = clientEndpoint,
                ConnectedAt = DateTime.Now,
                LastActivity = DateTime.Now,
                SessionStatus = "Connected"
            };

            _context.UserSessions.Add(userSession);
            _context.SaveChanges();
        }


        public void UpdateLastActivity(int userId)
        {
            var session = _context.UserSessions
                                  .FirstOrDefault(s => s.UserID == userId && s.SessionStatus == "Connected");

            if (session != null)
            {
                session.LastActivity = DateTime.Now;
                _context.SaveChanges();
            }
        }

        public void EndSession(int userId)
        {
            var session = _context.UserSessions
                                  .FirstOrDefault(s => s.UserID == userId && s.SessionStatus == "Connected");

            if (session != null)
            {
                session.SessionStatus = "Disconnected";
                _context.SaveChanges();
            }
        }

        public UserSession GetActiveSession(int userId)
        {
            return _context.UserSessions
                           .FirstOrDefault(s => s.UserID == userId && s.SessionStatus == "Connected");
        }

        public IQueryable<UserSession> GetUserSessions(int userId)
        {
            return _context.UserSessions.Where(s => s.UserID == userId);
        }

        public void CleanUpInactiveSessions(TimeSpan timeout)
        {
            var cutoffTime = DateTime.Now - timeout;

            var inactiveSessions = _context.UserSessions
                                            .Where(s => s.SessionStatus == "Connected" && s.LastActivity < cutoffTime);

            foreach (var session in inactiveSessions)
            {
                session.SessionStatus = "Inactive";
            }

            _context.SaveChanges();
        }
    }
}
