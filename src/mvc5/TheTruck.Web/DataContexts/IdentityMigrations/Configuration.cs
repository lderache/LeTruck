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
                // Roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "admin" };
                roleManager.Create(role);

                // Admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser { UserName = "laurent.derache@gmail.com" };
                userManager.Create(user, "password");

                // Add the role to admin user
                userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
