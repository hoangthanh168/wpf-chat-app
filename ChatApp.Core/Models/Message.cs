using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        // Foreign Keys
        public int SenderID { get; set; }
        public int? ReceiverID { get; set; }
        public int? GroupID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsGroupMessage { get; set; }

        // Navigation Properties
        [ForeignKey("SenderID")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public User Receiver { get; set; }

        [ForeignKey("GroupID")]
        public GroupChat GroupChat { get; set; }

        public ICollection<OfflineMessage> OfflineMessages { get; set; }
    }
}
