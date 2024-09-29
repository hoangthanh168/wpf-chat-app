using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.Models
{
    public class OfflineMessage
    {
        [Key]
        public int OfflineMessageID { get; set; }

        // Foreign Keys
        public int UserID { get; set; }
        public int MessageID { get; set; }

        // Navigation Properties
        [ForeignKey("UserID")]
        public User User { get; set; }

        [ForeignKey("MessageID")]
        public Message Message { get; set; }
    }

}
