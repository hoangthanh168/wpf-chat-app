using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.Models
{
    public class GroupMember
    {
        public int GroupID { get; set; }
        public int UserID { get; set; }

        public DateTime JoinedAt { get; set; }

        // Navigation Properties
        [ForeignKey("GroupID")]
        public GroupChat GroupChat { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
