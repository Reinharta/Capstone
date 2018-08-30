namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingexception : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationItems", "ImageFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationItems", "ImageFilePath");
        }
    }
}
