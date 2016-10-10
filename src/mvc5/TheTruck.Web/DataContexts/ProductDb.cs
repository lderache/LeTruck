using System.Data.Entity;
using TheTruck.Entities;

namespace TheTruck.Web.DataContexts
{
    public class ProductDb : DbContext
    {
        public ProductDb() : base("DefaultConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}