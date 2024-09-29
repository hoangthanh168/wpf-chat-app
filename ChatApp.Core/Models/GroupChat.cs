using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        // Navigation Properties
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
