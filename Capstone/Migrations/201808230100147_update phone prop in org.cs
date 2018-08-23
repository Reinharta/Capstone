namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatephonepropinorg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NonprofitOrganizations", "OrganizationPhone", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NonprofitOrganizations", "OrganizationPhone", c => c.Int(nullable: false));
        }
    }
}
