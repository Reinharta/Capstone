namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NonprofitOrganizations", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NonprofitOrganizations", "UserId");
        }
    }
}
