namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingCategoryTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrganizationCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.DonationItems", "ItemSize", c => c.String());
            AddColumn("dbo.DonationItems", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.DonationItems", "RequestingOrganizationId", c => c.Int(nullable: false));
            AddColumn("dbo.NonprofitOrganizations", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.DonationItems", "ItemName", c => c.String(nullable: false));
            CreateIndex("dbo.DonationItems", "CategoryId");
            CreateIndex("dbo.NonprofitOrganizations", "CategoryId");
            AddForeignKey("dbo.DonationItems", "CategoryId", "dbo.ItemCategories", "CategoryId", cascadeDelete: true);
            AddForeignKey("dbo.NonprofitOrganizations", "CategoryId", "dbo.OrganizationCategories", "CategoryId", cascadeDelete: true);
            DropColumn("dbo.DonationItems", "RequestingOrganization");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DonationItems", "RequestingOrganization", c => c.Int(nullable: false));
            DropForeignKey("dbo.NonprofitOrganizations", "CategoryId", "dbo.OrganizationCategories");
            DropForeignKey("dbo.DonationItems", "CategoryId", "dbo.ItemCategories");
            DropIndex("dbo.NonprofitOrganizations", new[] { "CategoryId" });
            DropIndex("dbo.DonationItems", new[] { "CategoryId" });
            AlterColumn("dbo.DonationItems", "ItemName", c => c.String());
            DropColumn("dbo.NonprofitOrganizations", "CategoryId");
            DropColumn("dbo.DonationItems", "RequestingOrganizationId");
            DropColumn("dbo.DonationItems", "CategoryId");
            DropColumn("dbo.DonationItems", "ItemSize");
            DropTable("dbo.OrganizationCategories");
            DropTable("dbo.ItemCategories");
        }
    }
}
