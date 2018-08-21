namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinguserpropinNP : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NonprofitOrganizations", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.NonprofitOrganizations", "UserId");
            AddForeignKey("dbo.NonprofitOrganizations", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NonprofitOrganizations", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.NonprofitOrganizations", new[] { "UserId" });
            AlterColumn("dbo.NonprofitOrganizations", "UserId", c => c.String());
        }
    }
}
