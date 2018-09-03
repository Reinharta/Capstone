namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedSupportermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supporters", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Supporters", "UserId");
            AddForeignKey("dbo.Supporters", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supporters", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Supporters", new[] { "UserId" });
            DropColumn("dbo.Supporters", "UserId");
        }
    }
}
