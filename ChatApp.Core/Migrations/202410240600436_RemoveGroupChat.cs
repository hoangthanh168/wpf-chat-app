namespace ChatApp.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGroupChat : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembers", "GroupID", "dbo.GroupChats");
            DropForeignKey("dbo.Messages", "GroupID", "dbo.GroupChats");
            DropForeignKey("dbo.GroupMembers", "UserID", "dbo.Users");
            DropIndex("dbo.GroupMembers", new[] { "GroupID" });
            DropIndex("dbo.GroupMembers", new[] { "UserID" });
            DropIndex("dbo.Messages", new[] { "GroupID" });
            DropColumn("dbo.Messages", "GroupID");
            DropColumn("dbo.Messages", "IsGroupMessage");
            DropTable("dbo.GroupChats");
            DropTable("dbo.GroupMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        JoinedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupID, t.UserID });
            
            CreateTable(
                "dbo.GroupChats",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false, maxLength: 100),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.GroupID);
            
            AddColumn("dbo.Messages", "IsGroupMessage", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "GroupID", c => c.Int());
            CreateIndex("dbo.Messages", "GroupID");
            CreateIndex("dbo.GroupMembers", "UserID");
            CreateIndex("dbo.GroupMembers", "GroupID");
            AddForeignKey("dbo.GroupMembers", "UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Messages", "GroupID", "dbo.GroupChats", "GroupID", cascadeDelete: true);
            AddForeignKey("dbo.GroupMembers", "GroupID", "dbo.GroupChats", "GroupID");
        }
    }
}
