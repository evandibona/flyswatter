namespace FlySwatter.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity; 
    using Microsoft.AspNet.Identity.EntityFramework;
    using FlySwatter.Models; 

    internal sealed class Configuration : DbMigrationsConfiguration<FlySwatter.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FlySwatter.Models.ApplicationDbContext context)
        {
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
            //Admin, Project Manager, Developer, Submitter
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context)); 
            if (!context.Roles.Any(r => r.Name == "Admin")) 
            {
                var result = roleManager.Create(new IdentityRole("Admin")); 
            }
            if (!context.Roles.Any(r => r.Name == "ProjectManager"))
            {
                var result = roleManager.Create(new IdentityRole("ProjectManager")); 
            }
            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                var result = roleManager.Create(new IdentityRole("Developer")); 
            }
            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                var result = roleManager.Create(new IdentityRole("Submitter")); 
            }
            if (!context.Users.Any(u => u.Email == "evandibona@gmail.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser
                {
                    UserName = "edibona",
                    Email = "evandibona@gmail.com",
                    FirstName = "Evan"
                };
                userManager.Create(user, "scythe");
                userManager.AddToRole(user.Id, "Admin"); 
            }
        }
    }
}
