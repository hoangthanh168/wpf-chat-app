using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.Models
{

    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [MaxLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastLogin { get; set; }

        [MaxLength(20)]
        public string Status { get; set; }

        // Navigation Properties
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<OfflineMessage> OfflineMessages { get; set; }
    }

}
