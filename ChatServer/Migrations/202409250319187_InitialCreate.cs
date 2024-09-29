namespace ChatServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupChats",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        JoinedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID })
                .ForeignKey("dbo.GroupChats", t => t.GroupID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.GroupID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                        DisplayName = c.String(maxLength: 100),
                        Email = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        LastLogin = c.DateTime(nullable: false),
                        Status = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.OfflineMessages",
                c => new
                    {
                        OfflineMessageID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        MessageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OfflineMessageID)
                .ForeignKey("dbo.Messages", t => t.MessageID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.MessageID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        SenderID = c.Int(nullable: false),
                        ReceiverID = c.Int(),
                        GroupID = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        SentAt = c.DateTime(nullable: false),
                        IsGroupMessage = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.GroupChats", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ReceiverID)
                .ForeignKey("dbo.Users", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiverID)
                .Index(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupMembers", "UserID", "dbo.Users");
            DropForeignKey("dbo.OfflineMessages", "UserID", "dbo.Users");
            DropForeignKey("dbo.OfflineMessages", "MessageID", "dbo.Messages");
            DropForeignKey("dbo.Messages", "SenderID", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverID", "dbo.Users");
            DropForeignKey("dbo.Messages", "GroupID", "dbo.GroupChats");
            DropForeignKey("dbo.GroupMembers", "GroupID", "dbo.GroupChats");
            DropIndex("dbo.Messages", new[] { "GroupID" });
            DropIndex("dbo.Messages", new[] { "ReceiverID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.OfflineMessages", new[] { "MessageID" });
            DropIndex("dbo.OfflineMessages", new[] { "UserID" });
            DropIndex("dbo.GroupMembers", new[] { "UserID" });
            DropIndex("dbo.GroupMembers", new[] { "GroupID" });
            DropTable("dbo.Messages");
            DropTable("dbo.OfflineMessages");
            DropTable("dbo.Users");
            DropTable("dbo.GroupMembers");
            DropTable("dbo.GroupChats");
        }
    }
}
