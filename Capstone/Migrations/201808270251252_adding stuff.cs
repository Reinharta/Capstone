namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingstuff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Latitude", c => c.Double());
            AlterColumn("dbo.Addresses", "Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Longitude", c => c.String());
            AlterColumn("dbo.Addresses", "Latitude", c => c.String());
        }
    }
}
