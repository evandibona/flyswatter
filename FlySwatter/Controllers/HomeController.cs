using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FlySwatter.Models;

namespace FlySwatter.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);
            var sorters = new Dictionary<string, char>(); 
            foreach (var a in new string[] {"Priority", "Name", "Project", "Created", "Description"})
            {
                sorters.Add(a, '0'); 
            }
            var model = new TicketTableViewModel() { Sorters = sorters, Tickets = tickets.ToList() }; 
            return View(model);
        }
    }
}