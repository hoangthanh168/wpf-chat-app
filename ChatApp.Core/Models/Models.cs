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

        public int SenderID { get; set; }
        public int? ReceiverID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        [ForeignKey("SenderID")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public User Receiver { get; set; }
    }

    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastLogin { get; set; }

        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
    }

}
