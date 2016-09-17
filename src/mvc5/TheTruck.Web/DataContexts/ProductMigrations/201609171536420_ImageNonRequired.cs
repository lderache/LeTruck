namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageNonRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Image", c => c.String(nullable: false));
        }
    }
}
