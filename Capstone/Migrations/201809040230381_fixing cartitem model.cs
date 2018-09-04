namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingcartitemmodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CartItems", "BasektId", "dbo.DonationBaskets");
            DropIndex("dbo.CartItems", new[] { "BasektId" });
            RenameColumn(table: "dbo.CartItems", name: "BasektId", newName: "BasketId");
            AlterColumn("dbo.CartItems", "BasketId", c => c.Int());
            CreateIndex("dbo.CartItems", "BasketId");
            AddForeignKey("dbo.CartItems", "BasketId", "dbo.DonationBaskets", "BasketId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "BasketId", "dbo.DonationBaskets");
            DropIndex("dbo.CartItems", new[] { "BasketId" });
            AlterColumn("dbo.CartItems", "BasketId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.CartItems", name: "BasketId", newName: "BasektId");
            CreateIndex("dbo.CartItems", "BasektId");
            AddForeignKey("dbo.CartItems", "BasektId", "dbo.DonationBaskets", "BasketId", cascadeDelete: true);
        }
    }
}
