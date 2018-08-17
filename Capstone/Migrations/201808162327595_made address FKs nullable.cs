namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeaddressFKsnullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NonprofitOrganizations", "DropOffAddress", "dbo.Addresses");
            DropForeignKey("dbo.NonprofitOrganizations", "ShippingAddress", "dbo.Addresses");
            DropForeignKey("dbo.Supporters", "SupporterAddress", "dbo.Addresses");
            DropIndex("dbo.NonprofitOrganizations", new[] { "ShippingAddress" });
            DropIndex("dbo.NonprofitOrganizations", new[] { "DropOffAddress" });
            DropIndex("dbo.Supporters", new[] { "SupporterAddress" });
            AlterColumn("dbo.NonprofitOrganizations", "ShippingAddress", c => c.Int());
            AlterColumn("dbo.NonprofitOrganizations", "DropOffAddress", c => c.Int());
            AlterColumn("dbo.Supporters", "SupporterAddress", c => c.Int());
            CreateIndex("dbo.NonprofitOrganizations", "ShippingAddress");
            CreateIndex("dbo.NonprofitOrganizations", "DropOffAddress");
            CreateIndex("dbo.Supporters", "SupporterAddress");
            AddForeignKey("dbo.NonprofitOrganizations", "DropOffAddress", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.NonprofitOrganizations", "ShippingAddress", "dbo.Addresses", "AddressId");
            AddForeignKey("dbo.Supporters", "SupporterAddress", "dbo.Addresses", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Supporters", "SupporterAddress", "dbo.Addresses");
            DropForeignKey("dbo.NonprofitOrganizations", "ShippingAddress", "dbo.Addresses");
            DropForeignKey("dbo.NonprofitOrganizations", "DropOffAddress", "dbo.Addresses");
            DropIndex("dbo.Supporters", new[] { "SupporterAddress" });
            DropIndex("dbo.NonprofitOrganizations", new[] { "DropOffAddress" });
            DropIndex("dbo.NonprofitOrganizations", new[] { "ShippingAddress" });
            AlterColumn("dbo.Supporters", "SupporterAddress", c => c.Int(nullable: false));
            AlterColumn("dbo.NonprofitOrganizations", "DropOffAddress", c => c.Int(nullable: false));
            AlterColumn("dbo.NonprofitOrganizations", "ShippingAddress", c => c.Int(nullable: false));
            CreateIndex("dbo.Supporters", "SupporterAddress");
            CreateIndex("dbo.NonprofitOrganizations", "DropOffAddress");
            CreateIndex("dbo.NonprofitOrganizations", "ShippingAddress");
            AddForeignKey("dbo.Supporters", "SupporterAddress", "dbo.Addresses", "AddressId", cascadeDelete: true);
            AddForeignKey("dbo.NonprofitOrganizations", "ShippingAddress", "dbo.Addresses", "AddressId", cascadeDelete: true);
            AddForeignKey("dbo.NonprofitOrganizations", "DropOffAddress", "dbo.Addresses", "AddressId", cascadeDelete: true);
        }
    }
}
