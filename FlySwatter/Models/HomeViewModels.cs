using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PagedList; 

namespace FlySwatter.Models
{
    public class HomeView
    {
        public IPagedList<Ticket> Tickets { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Project> Projects { get; set; }

        public string SearchString { get; set; }
        public string SortOrder { get; set; }
        public string Relationship { get; set; } 
    }
}