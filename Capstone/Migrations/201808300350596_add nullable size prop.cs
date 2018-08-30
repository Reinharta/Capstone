namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnullablesizeprop : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DonationItems", "ItemQuantity", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DonationItems", "ItemQuantity", c => c.Int(nullable: false));
        }
    }
}
