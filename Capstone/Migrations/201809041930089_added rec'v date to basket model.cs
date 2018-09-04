namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrecvdatetobasketmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationBaskets", "ReceivedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationBaskets", "ReceivedDate");
        }
    }
}
