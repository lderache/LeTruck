namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSurvey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        OrderFrequency = c.Int(nullable: false),
                        StayPeriod = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Surveys");
        }
    }
}
