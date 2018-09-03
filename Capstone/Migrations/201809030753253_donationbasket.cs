namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donationbasket : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartItems");
            AddColumn("dbo.CartItems", "CartItemId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CartItems", "SupporterId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CartItems", "CartItemId");
            CreateIndex("dbo.CartItems", "SupporterId");
            AddForeignKey("dbo.CartItems", "SupporterId", "dbo.Supporters", "SupporterId", cascadeDelete: true);
            DropColumn("dbo.CartItems", "BasketItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "BasketItemId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.CartItems", "SupporterId", "dbo.Supporters");
            DropIndex("dbo.CartItems", new[] { "SupporterId" });
            DropPrimaryKey("dbo.CartItems");
            DropColumn("dbo.CartItems", "SupporterId");
            DropColumn("dbo.CartItems", "CartItemId");
            AddPrimaryKey("dbo.CartItems", "BasketItemId");
        }
    }
}
