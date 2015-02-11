using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace FlySwatter.Models
{
    public class HomeView
    {
        public List<Ticket> Tickets { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }

        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public string Relationship { get; set; } 
    }
}