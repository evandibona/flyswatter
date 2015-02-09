using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlySwatter.Models; 

namespace FlySwatter.Controllers
{
    public class TicketTableController : Controller
    {
        // get :: Table
        [Authorize]
        public ActionResult Index()
        {
            var model = new TicketTableViewModel();
            foreach (var a in new string[] { "Assigned", "Name", "Created" })
            {
                model.sorters[a] = '0'; 
            }
            return View();
        }
    }
}