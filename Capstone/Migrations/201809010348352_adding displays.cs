namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingdisplays : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonationItems", "ItemDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DonationItems", "ItemDescription", c => c.String(nullable: false));
        }
    }
}
