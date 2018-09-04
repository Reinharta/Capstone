namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeah : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "CartItemId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CartItems", "CartItemId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CartItems");
            AlterColumn("dbo.CartItems", "CartItemId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.CartItems", "CartItemId");
        }
    }
}
