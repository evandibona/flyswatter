using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FlySwatter.Models
{
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
        public virtual DbSet<BugUser>               BugUsers { get; set; }
        public virtual DbSet<Ticket>                Tickets { get; set; }
        public virtual DbSet<TicketAttachment>      TicketAttachments { get; set; }
        public virtual DbSet<TicketComment>         TicketComments { get; set; }
        public virtual DbSet<TicketHistory>         TicketHistories { get; set; }
        public virtual DbSet<TicketNotification>    TicketNotifications { get; set; }
        public virtual DbSet<TicketPriority>        TicketPriorities { get; set; }
        public virtual DbSet<TicketStatus> TicketStatus { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; } 
    }
}