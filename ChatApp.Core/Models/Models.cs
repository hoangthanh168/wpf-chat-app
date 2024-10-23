using ChatApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Models
{
    public class GroupChat
    {
        [Key]
        public int GroupID { get; set; }

        [Required]
        [MaxLength(100)]
        public string GroupName { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }

    public class GroupMember
    {
        public int GroupID { get; set; }
        public int UserID { get; set; }

        public DateTime JoinedAt { get; set; }

        [ForeignKey("GroupID")]
        public GroupChat GroupChat { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }

    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        public int SenderID { get; set; }
        public int? ReceiverID { get; set; }
        public int? GroupID { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public bool IsGroupMessage { get; set; }

        [ForeignKey("SenderID")]
        public User Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public User Receiver { get; set; }

        [ForeignKey("GroupID")]
        public GroupChat GroupChat { get; set; }

        public ICollection<OfflineMessage> OfflineMessages { get; set; }
    }

    public class OfflineMessage
    {
        [Key]
        public int OfflineMessageID { get; set; }

        public int UserID { get; set; }
        public int MessageID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("MessageID")]
        public Message Message { get; set; }
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

        [MaxLength(20)]
        public string Status { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<OfflineMessage> OfflineMessages { get; set; }
    }
   
}
