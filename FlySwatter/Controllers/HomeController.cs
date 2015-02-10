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
        public ActionResult Index(string id)
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);

            switch (id)
            {
                case "title_desc":
                    tickets = tickets.OrderByDescending(t => t.Title);
                    break; 
                case "Created":
                    tickets = tickets.OrderBy(t => t.Created);
                    break; 
                case "created_desc":
                    tickets = tickets.OrderByDescending(t => t.Created);
                    break; 
                default:
                    tickets = tickets.OrderBy(t => t.Title); 
                    break;
            }

            var model = new HomeView() { Tickets = tickets.ToList() }; 
            return View(model);
        }
    }
}