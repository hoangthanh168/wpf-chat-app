using ChatApp.Core.Models;
using System.Data.Entity;

namespace ChatApp.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=ThanhConnection")
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

            modelBuilder.Entity<GroupMember>()
                .HasKey(gm => new { gm.GroupID, gm.UserID });

            modelBuilder.Entity<GroupMember>()
                .HasRequired(gm => gm.User)
                .WithMany(u => u.GroupMembers)
                .HasForeignKey(gm => gm.UserID)
                .WillCascadeOnDelete(false);

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
                .WillCascadeOnDelete(true);

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
