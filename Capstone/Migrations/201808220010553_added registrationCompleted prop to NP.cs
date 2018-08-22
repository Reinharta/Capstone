namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedregistrationCompletedproptoNP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NonprofitOrganizations", "RegistrationCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NonprofitOrganizations", "RegistrationCompleted");
        }
    }
}
