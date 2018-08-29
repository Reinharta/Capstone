namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletedDonationItemmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationItems", "Brand", c => c.String());
            AddColumn("dbo.DonationItems", "Color", c => c.String());
            DropColumn("dbo.DonationItems", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonationItems", "MyProperty", c => c.Int(nullable: false));
            DropColumn("dbo.DonationItems", "Color");
            DropColumn("dbo.DonationItems", "Brand");
        }
    }
}
