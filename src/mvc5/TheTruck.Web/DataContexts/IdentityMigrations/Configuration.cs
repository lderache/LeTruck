namespace TheTruck.Web.DataContexts.IdentityMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Configuration;
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
            // ADMIN
            var adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            var adminPassword = ConfigurationManager.AppSettings["adminPassword"];

            if (!context.Users.Any(u => u.UserName == adminEmail))
            {
                // Roles
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "admin" };
                roleManager.Create(role);

                // Admin user
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var administrator = new ApplicationUser { UserName = adminEmail, EmailConfirmed = true };
                userManager.Create(administrator, adminPassword);

                // Add the role to admin user
                userManager.AddToRole(administrator.Id, role.Name);
            }

            var userEmail = ConfigurationManager.AppSettings["userEmail"];
            var userPassword = ConfigurationManager.AppSettings["userPassword"];

            // USER
            if (!context.Users.Any(u => u.UserName == userEmail))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser { UserName = userEmail, EmailConfirmed = true };
                userManager.Create(user, userPassword);
            }
        }
    }
}
