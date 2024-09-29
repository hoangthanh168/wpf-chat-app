using ChatApp.Core.Models;
using System.Data.Entity;

namespace ChatServer
{
    public class AppDbContext : DbContext
    {
        // Constructor truyền vào tên chuỗi kết nối từ Web.config hoặc App.config
        public AppDbContext()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<GroupChat> GroupChats { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<OfflineMessage> OfflineMessages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính hợp nhất cho GroupMember
            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.GroupID, gm.UserID });

            // Cấu hình quan hệ giữa GroupMember và User
            modelBuilder.Entity<GroupMember>()
                .HasRequired(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserID)
                .WillCascadeOnDelete(false);

            // Cấu hình quan hệ giữa GroupMember và GroupChat
            modelBuilder.Entity<GroupMember>()
                .HasRequired(gm => gm.GroupChat)
                .WithMany(gc => gc.GroupMembers)
                .HasForeignKey(gm => gm.GroupID)
                .WillCascadeOnDelete(false);

            // Cấu hình quan hệ cho Message
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasOptional(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.GroupChat)
                .WithMany(gc => gc.Messages)
                .HasForeignKey(m => m.GroupID)
                .WillCascadeOnDelete(true); // Cascade delete cho GroupChat

            // Cấu hình quan hệ cho OfflineMessage
            modelBuilder.Entity<OfflineMessage>()
                .HasRequired(om => om.User)
                .WithMany(u => u.OfflineMessages)
                .HasForeignKey(om => om.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OfflineMessage>()
                .HasRequired(om => om.Message)
                .WithMany(m => m.OfflineMessages)
                .HasForeignKey(om => om.MessageID)
                .WillCascadeOnDelete(false);
        }
    }
}
