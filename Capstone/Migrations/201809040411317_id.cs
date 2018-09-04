namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class id : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartItems");
            AddColumn("dbo.CartItems", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CartItems", "Id");
            DropColumn("dbo.CartItems", "CartItemId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CartItems", "CartItemId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.CartItems");
            DropColumn("dbo.CartItems", "Id");
            AddPrimaryKey("dbo.CartItems", "CartItemId");
        }
    }
}
