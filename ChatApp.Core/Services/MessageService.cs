﻿using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApp.Core.Services
{
    public class MessageService
    {
        private readonly AppDbContext _context;

        public MessageService(AppDbContext context)
        {
            _context = context;
        }

        public void SaveMessage(Message message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();
        }

        public List<Message> GetMessagesByGroupId(int groupId)
        {
            return _context.Messages.Where(m => m.GroupID == groupId).ToList();
        }

        public List<Message> GetPrivateMessages(int senderId, int receiverId)
        {
            return _context.Messages?.Where(m => m.SenderID == senderId && m.ReceiverID == receiverId).ToList();
        }

        public void SaveOfflineMessage(OfflineMessage offlineMessage)
        {
            _context.OfflineMessages.Add(offlineMessage);
            _context.SaveChanges();
        }
    }
}
