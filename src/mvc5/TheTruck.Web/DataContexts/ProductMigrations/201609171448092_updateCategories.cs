namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCategories : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Discount", c => c.Int(nullable: false));
        }
    }
}
