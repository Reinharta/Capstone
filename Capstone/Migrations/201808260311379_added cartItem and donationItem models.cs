namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcartItemanddonationItemmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        CartId = c.String(),
                        Quantity = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Product_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.DonationItems", t => t.Product_ItemId)
                .Index(t => t.Product_ItemId);
            
            CreateTable(
                "dbo.DonationItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        ItemQuantity = c.Int(nullable: false),
                        ItemDescription = c.String(),
                        RequestingOrganization = c.Int(nullable: false),
                        MyProperty = c.Int(nullable: false),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.NonprofitOrganizations", t => t.Organization_OrganizationId)
                .Index(t => t.Organization_OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Product_ItemId", "dbo.DonationItems");
            DropForeignKey("dbo.DonationItems", "Organization_OrganizationId", "dbo.NonprofitOrganizations");
            DropIndex("dbo.DonationItems", new[] { "Organization_OrganizationId" });
            DropIndex("dbo.CartItems", new[] { "Product_ItemId" });
            DropTable("dbo.DonationItems");
            DropTable("dbo.CartItems");
        }
    }
}
