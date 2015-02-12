using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlySwatter.Models;
using FlySwatter.Helpers;

namespace FlySwatter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index(string sortOrder, string searchString, string Owner, string Assigned, string Project)
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);
            var sortParams = new Dictionary<string,string>();

            tickets = tickets.search(searchString); 
            tickets = tickets.filterBigguns(Owner, Assigned, Project);
            tickets = tickets.SortColumns(sortOrder); 

            var model = new HomeView() { Tickets = tickets.ToList() };
            model.Users = db.Users.ToList(); 
            model.Projects = db.Projects.ToList(); 
            return View(model);
        }
    }
}