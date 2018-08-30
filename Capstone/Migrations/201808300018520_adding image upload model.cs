namespace Capstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingimageuploadmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageUploads",
                c => new
                    {
                        ImageFileId = c.Int(nullable: false, identity: true),
                        File = c.String(),
                    })
                .PrimaryKey(t => t.ImageFileId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImageUploads");
        }
    }
}
