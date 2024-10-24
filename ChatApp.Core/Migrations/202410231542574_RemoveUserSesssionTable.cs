namespace ChatApp.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserSesssionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSessions", "UserID", "dbo.Users");
            DropIndex("dbo.UserSessions", new[] { "UserID" });
            DropTable("dbo.UserSessions");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.SessionID);
            
            CreateIndex("dbo.UserSessions", "UserID");
            AddForeignKey("dbo.UserSessions", "UserID", "dbo.Users", "UserID");
        }
    }
}
