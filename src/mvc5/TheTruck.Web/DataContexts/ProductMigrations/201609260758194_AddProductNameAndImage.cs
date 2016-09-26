namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductNameAndImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Image", c => c.String());
            AddColumn("dbo.OrderItems", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Name");
            DropColumn("dbo.OrderItems", "Image");
        }
    }
}
