namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingtoIdentityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "OrganizationName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SupporterFirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SupporterLastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SupporterLastName");
            DropColumn("dbo.AspNetUsers", "SupporterFirstName");
            DropColumn("dbo.AspNetUsers", "OrganizationName");
        }
    }
}
