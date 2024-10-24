namespace ChatApp.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveOfflineMessage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OfflineMessages", "User_UserID", "dbo.Users");
            DropIndex("dbo.OfflineMessages", new[] { "User_UserID" });
            DropTable("dbo.OfflineMessages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OfflineMessages",
                c => new
                    {
                        OfflineMessageID = c.Int(nullable: false, identity: true),
                        SenderID = c.Int(nullable: false),
                        ReceiverID = c.Int(),
                        Content = c.String(),
                        SentAt = c.DateTime(nullable: false),
                        IsGroupMessage = c.Boolean(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.OfflineMessageID);
            
            CreateIndex("dbo.OfflineMessages", "User_UserID");
            AddForeignKey("dbo.OfflineMessages", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
