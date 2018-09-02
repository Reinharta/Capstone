namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDonationBasketupdatingcartItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "Product_ItemId", "dbo.DonationItems");
            DropIndex("dbo.CartItems", new[] { "Product_ItemId" });
            DropColumn("dbo.CartItems", "ProductId");
            RenameColumn(table: "dbo.CartItems", name: "Product_ItemId", newName: "ProductId");
            DropPrimaryKey("dbo.CartItems");
            CreateTable(
                "dbo.DonationBaskets",
                c => new
                    {
                        BasketId = c.Int(nullable: false, identity: true),
                        SupporterId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        BasketPending = c.Boolean(nullable: false),
                        Received = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BasketId)
                .ForeignKey("dbo.NonprofitOrganizations", t => t.OrganizationId, cascadeDelete: false)
                .ForeignKey("dbo.Supporters", t => t.SupporterId, cascadeDelete: false)
                .Index(t => t.SupporterId)
                .Index(t => t.OrganizationId);
            
            AddColumn("dbo.CartItems", "BasketItemId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CartItems", "BasektId", c => c.Int(nullable: false));
            AlterColumn("dbo.CartItems", "ProductId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CartItems", "BasketItemId");
            CreateIndex("dbo.CartItems", "BasektId");
            CreateIndex("dbo.CartItems", "ProductId");
            AddForeignKey("dbo.CartItems", "BasektId", "dbo.DonationBaskets", "BasketId", cascadeDelete: true);
            AddForeignKey("dbo.CartItems", "ProductId", "dbo.DonationItems", "ItemId", cascadeDelete: true);
            DropColumn("dbo.CartItems", "ItemId");
            DropColumn("dbo.CartItems", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "CartId", c => c.String());
            AddColumn("dbo.CartItems", "ItemId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.CartItems", "ProductId", "dbo.DonationItems");
            DropForeignKey("dbo.CartItems", "BasektId", "dbo.DonationBaskets");
            DropForeignKey("dbo.DonationBaskets", "SupporterId", "dbo.Supporters");
            DropForeignKey("dbo.DonationBaskets", "OrganizationId", "dbo.NonprofitOrganizations");
            DropIndex("dbo.DonationBaskets", new[] { "OrganizationId" });
            DropIndex("dbo.DonationBaskets", new[] { "SupporterId" });
            DropIndex("dbo.CartItems", new[] { "ProductId" });
            DropIndex("dbo.CartItems", new[] { "BasektId" });
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "ProductId", c => c.Int());
            DropColumn("dbo.CartItems", "BasektId");
            DropColumn("dbo.CartItems", "BasketItemId");
            DropTable("dbo.DonationBaskets");
            AddPrimaryKey("dbo.CartItems", "ItemId");
            RenameColumn(table: "dbo.CartItems", name: "ProductId", newName: "Product_ItemId");
            AddColumn("dbo.CartItems", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.CartItems", "Product_ItemId");
            AddForeignKey("dbo.CartItems", "Product_ItemId", "dbo.DonationItems", "ItemId");
        }
    }
}
