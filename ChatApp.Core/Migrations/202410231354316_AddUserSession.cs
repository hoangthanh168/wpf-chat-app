namespace ChatApp.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSession : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSessions",
                c => new
                    {
                        SessionID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ClientEndpoint = c.String(),
                        ConnectedAt = c.DateTime(nullable: false),
                        LastActivity = c.DateTime(nullable: false),
                        SessionStatus = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.SessionID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            DropColumn("dbo.Users", "DisplayName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "DisplayName", c => c.String(maxLength: 100));
            DropForeignKey("dbo.UserSessions", "UserID", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "UserID" });
            DropTable("dbo.UserSessions");
        }
    }
}
