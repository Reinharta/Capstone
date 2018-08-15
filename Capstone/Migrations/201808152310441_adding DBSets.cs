namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingDBSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        ContactPerson = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.NonprofitOrganizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        OrganizationName = c.String(),
                        ShippingAddress = c.Int(nullable: false),
                        DropOffAddress = c.Int(nullable: false),
                        OrganizationDescription = c.String(maxLength: 250),
                        OrganizationWebsite = c.String(),
                        OrganizationPhone = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId)
                .ForeignKey("dbo.Addresses", t => t.DropOffAddress, cascadeDelete: false)
                .ForeignKey("dbo.Addresses", t => t.ShippingAddress, cascadeDelete: false)
                .Index(t => t.ShippingAddress)
                .Index(t => t.DropOffAddress);
            
            CreateTable(
                "dbo.Supporters",
                c => new
                    {
                        SupporterId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        SupporterAddress = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SupporterId)
                .ForeignKey("dbo.Addresses", t => t.SupporterAddress, cascadeDelete: false)
                .Index(t => t.SupporterAddress);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supporters", "SupporterAddress", "dbo.Addresses");
            DropForeignKey("dbo.NonprofitOrganizations", "ShippingAddress", "dbo.Addresses");
            DropForeignKey("dbo.NonprofitOrganizations", "DropOffAddress", "dbo.Addresses");
            DropIndex("dbo.Supporters", new[] { "SupporterAddress" });
            DropIndex("dbo.NonprofitOrganizations", new[] { "DropOffAddress" });
            DropIndex("dbo.NonprofitOrganizations", new[] { "ShippingAddress" });
            DropTable("dbo.Supporters");
            DropTable("dbo.NonprofitOrganizations");
            DropTable("dbo.Addresses");
        }
    }
}
