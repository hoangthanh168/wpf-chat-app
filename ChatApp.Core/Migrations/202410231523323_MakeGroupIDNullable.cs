namespace ChatApp.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeGroupIDNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "GroupID" });
            AlterColumn("dbo.Messages", "GroupID", c => c.Int());
            CreateIndex("dbo.Messages", "GroupID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "GroupID" });
            AlterColumn("dbo.Messages", "GroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.Messages", "GroupID");
        }
    }
}
