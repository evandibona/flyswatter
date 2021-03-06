﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace FlySwatter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public ApplicationUser()
        {
            this.AssignedTickets = new HashSet<Ticket>();
            this.OwnedTickets = new HashSet<Ticket>();
            this.Projects = new HashSet<Project>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Ticket> AssignedTickets { get; set; }
        public virtual ICollection<Ticket> OwnedTickets { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Project>               Projects { get; set; }
        public virtual DbSet<Ticket>                Tickets { get; set; }
        public virtual DbSet<TicketAttachment>      TicketAttachments { get; set; }
        public virtual DbSet<TicketComment>         TicketComments { get; set; }
        public virtual DbSet<TicketHistory>         TicketHistories { get; set; }
        public virtual DbSet<TicketNotification>    TicketNotifications { get; set; }
        public virtual DbSet<TicketPriority>        TicketPriorities { get; set; }
        public virtual DbSet<TicketStatus>          TicketStatus { get; set; }
        public virtual DbSet<TicketType>            TicketTypes { get; set; }
    }
}