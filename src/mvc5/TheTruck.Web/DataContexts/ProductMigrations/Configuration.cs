namespace TheTruck.Web.DataContexts.ProductMigrations
{
    using Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<TheTruck.Web.DataContexts.ProductDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\ProductMigrations";
        }

        protected override void Seed(TheTruck.Web.DataContexts.ProductDb context)
        {

            var products = new List<Product>
            {
                new Product { Category = Genre.Confectionary, Name = "Baguette", Price = 15.6m, Description = "La baguette Baguepi!" },
                new Product { Category = Genre.Gourmet, Name = "Camembert", Price = 55, Description = "Le camembert coeur de lion!" }
            };

            products.ForEach(p => context.Products.AddOrUpdate(x => x.Name, p));

            context.SaveChanges();
        }
    }
}
