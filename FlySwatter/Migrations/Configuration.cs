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
            AutomaticMigrationDataLossAllowed = true;
        }

        private void seedRoles(ApplicationDbContext context)
        {
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
        }

        private void seedTicketStatuses(ApplicationDbContext context)
        {
            if (!context.TicketStatus.Any(s => s.Name == "Complete"))
            {
                var r = context.TicketStatus.Add(new TicketStatus{Name = "Complete"}); 
            }
            if (!context.TicketStatus.Any(s => s.Name == "In Progress"))
            {
                var r = context.TicketStatus.Add(new TicketStatus{Name = "In Progress"}); 
            }
        }
        private void seedTicketTypes(ApplicationDbContext context)
        {
            var names = new string[] { "Bug", "Feature", "Ideas" };
            foreach (string n in names)
            {
                if (!context.TicketTypes.Any(t => t.Name == n))
                {
                    var r = context.TicketTypes.Add(new TicketType { Name = n }); 
                }
            }
        }
        private void seedTicketPriorities(ApplicationDbContext context)
        {

        }
        protected override void Seed(FlySwatter.Models.ApplicationDbContext context)
        {
            seedRoles(context);
            seedTicketStatuses(context);
            seedTicketTypes(context); 
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context)); 
            var myEmail = "evandibona@gmail.com"; 
            if (!context.Users.Any(u => u.Email == myEmail))
            {
                userManager.Create(new ApplicationUser 
                { 
                    UserName = myEmail, 
                    Email = myEmail, 
                }, "scythe"); 
            }
            var userId = userManager.FindByEmail(myEmail).Id;
            userManager.AddToRole(userId, "Admin"); 
        }
    }
}
