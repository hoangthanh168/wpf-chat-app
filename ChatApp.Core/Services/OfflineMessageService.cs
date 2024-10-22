using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class OfflineMessageService
    {
        private readonly AppDbContext _context;

        public OfflineMessageService(AppDbContext context)
        {
            _context = context;
        }

        public List<OfflineMessage> GetOfflineMessagesByUserId(int userId)
        {
            return _context.OfflineMessages.Where(om => om.UserID == userId).ToList();
        }

        // Remove offline message after delivery
        public void RemoveOfflineMessage(int offlineMessageId)
        {
            var offlineMessage = _context.OfflineMessages.Find(offlineMessageId);
            if (offlineMessage != null)
            {
                _context.OfflineMessages.Remove(offlineMessage);
                _context.SaveChanges();
            }
        }
    }
}
