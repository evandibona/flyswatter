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
        public ActionResult Index(string sortOrder, string searchString, string Owner)
        {
            var tickets = db.Tickets.Include(t => t.AssignedUser).Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketStatus).Include(t => t.TicketType);
            var sortParams = new Dictionary<string,string>();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToString().Trim();
                tickets = tickets.Where(t => t.Title.Contains(searchString)
                        || t.Description.Contains(searchString) 
                        || t.OwnerUser.Email.Contains(searchString) 
                        || t.Project.Name.Contains(searchString) 
                        || t.TicketType.Name.Contains(searchString) 
                    ); 
            }

            switch (sortOrder)
            {
                case "title":
                    tickets = tickets.OrderBy(t => t.Title);
                    break; 
                case "title_desc":
                    tickets = tickets.OrderByDescending(t => t.Title);
                    break; 
                case "created":
                    tickets = tickets.OrderBy(t => t.Created);
                    break; 
                case "created_desc":
                    tickets = tickets.OrderByDescending(t => t.Created);
                    break; 
                case "priority":
                    tickets = tickets.OrderBy(t => t.TicketPriority.Name);
                    break; 
                case "priority_desc":
                    tickets = tickets.OrderByDescending(t => t.TicketPriority.Name);
                    break; 
                case "project":
                    tickets = tickets.OrderBy(t => t.Project.Name);
                    break; 
                default:
                    tickets = tickets.OrderBy(t => t.Title); 
                    break;
            }

            var model = new HomeView() { Tickets = tickets.ToList() };
            model.Users = db.Users.ToList(); 
            model.Projects = db.Projects.ToList(); 
            return View(model);
        }
    }
}