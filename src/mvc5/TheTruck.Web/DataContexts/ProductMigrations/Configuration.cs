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
            var beverages = new List<Product>
            {
                new Product { Category =  Genre.Beverage, Name = "Mango Juice", Price = 30m, Image = "/images/mango-juice.png"  },
                new Product { Category =  Genre.Beverage, Name = "Apple Juice", Price = 30m, Image = "/images/apple-juice.png"  },
                new Product { Category =  Genre.Beverage, Name = "Peach Juice", Price = 30m, Image = "/images/peach-juice.png"  },
                new Product { Category =  Genre.Beverage, Name = "Orange Juice", Price = 30m, Image = "/images/orange-juice.png" },
            };

            var dairy = new List<Product>
            {
                new Product { Category = Genre.Dairy, Name = "Brie Milkana", Price = 60m, Image = "/images/brie-milkana.png"  },
                new Product { Category = Genre.Dairy, Name = "Brie Fermier", Price = 60m, Image = "/images/brie-fermier.png"  },
                new Product { Category = Genre.Dairy, Name = "Coeur de Lion", Price = 60m, Image = "/images/coeur-de-lion.png" },
                new Product { Category = Genre.Dairy, Name = "Le Camembert", Price = 60m, Image = "/images/camembert.png"  },
            };

            var products = new List<Product>();
            products.AddRange(beverages);
            products.AddRange(dairy);

            products.ForEach(p => context.Products.AddOrUpdate(x => x.Name, p));

            context.SaveChanges();
        }
    }
}
