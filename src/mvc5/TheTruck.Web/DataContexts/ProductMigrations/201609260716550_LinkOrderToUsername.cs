namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkOrderToUsername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Username", c => c.String());
            DropColumn("dbo.Orders", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Username");
        }
    }
}
