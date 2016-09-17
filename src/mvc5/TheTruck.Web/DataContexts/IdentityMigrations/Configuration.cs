namespace TheTruck.Web.DataContexts.IdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheTruck.Web.DataContexts.IdentityDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"DataContexts\IdentityMigrations";
        }

        protected override void Seed(TheTruck.Web.DataContexts.IdentityDb context)
        {

            if (!context.Users.Any(u => u.UserName == "laurent.derache@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                manager.Create(
                    new ApplicationUser { UserName = "laurent.derache@gmail.com" }, "password");
            }

            /*
            var hasher = new PasswordHasher();

            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser { UserName = "laurent.derache@gmail.com", Email = "laurent.derache@gmail.com", PasswordHash = hasher.HashPassword("rHk6eGMX;") },
                new ApplicationUser { UserName = "lderache@hotmail.com", Email = "lderache@hotmail.com", PasswordHash = hasher.HashPassword("rHk6eGMX!") }
                );

            context.SaveChanges();
            */

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
