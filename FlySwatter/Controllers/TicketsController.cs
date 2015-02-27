using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using FlySwatter.Models;


namespace FlySwatter.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ManageController mc = new ManageController();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets;
            return RedirectToActionPermanent("Index", "Home");
        }

        // GET: Tickets/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            var tickets = db.Tickets.Include(t => t.TicketComments).Include(t => t.TicketAttachments);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = tickets.First(t => t.Id == id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Details/5
        [HttpPost]
        public ActionResult Details(string commentBody, Ticket ticket, string pictureUrl, string aDescription)
        {
            if (commentBody != null)
            {
                var date = DateTimeOffset.UtcNow;
                var authorId = User.Identity.GetUserId();
                var ticketId = ticket.Id;
                var comment = new TicketComment()
                {
                    Created = date,
                    TicketId = ticketId,
                    UserId = authorId,
                    Comment = commentBody
                };
                db.TicketComments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticketId });
            }
            if (pictureUrl != null)
            {
                var userId = User.Identity.GetUserId();
                var url = pictureUrl;
                var created = DateTimeOffset.UtcNow;
                var description = aDescription ?? "";
                var attachment = new TicketAttachment()
                {
                    UserId = userId,
                    Description = description,
                    FileUrl = url,
                    FilePath = "",
                    Created = created,
                    TicketId = ticket.Id,
                };
                db.TicketAttachments.Add(attachment);
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticket.Id });
            }
            return View();
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Created = DateTimeOffset.Now;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToActionPermanent("Index", "Home");
            }

            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "FirstName", ticket.AssignedUserId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatus, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            ViewBag.AssignedUserId = new SelectList(db.Users, "Id", "Email", ticket.AssignedUserId);
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "Email", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            TempData["ticket"] = ticket;
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Created,Updated,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var newTicket = ticket;
                Ticket oldTicket = (Ticket)TempData["ticket"];
                var newHistories = new List<TicketHistory>();

                //Iterate properties adding to histories things that have changes (using oldTicket.TicketHistories) then drop newTicket
                if (newTicket.Title != oldTicket.Title)
                {
                    newHistories.Add( new TicketHistory()
                        {
                            Property = "Title",
                            Changed = DateTimeOffset.UtcNow,
                            OldValue = oldTicket.Title,
                            NewValue = newTicket.Title,
                            TicketId = oldTicket.Id, 
                            UserId = User.Identity.GetUserId() 
                        });
                    oldTicket.Title = newTicket.Title; 
                }

                if (newHistories.Count > 0)
                {
                    foreach (var history in newHistories)
                    {
                        oldTicket.TicketHistories.Add(history); 
                    }
                }

                oldTicket.Updated = DateTimeOffset.UtcNow;
                db.Entry(oldTicket).State = EntityState.Modified;
                db.SaveChanges();
                return View(oldTicket);
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToActionPermanent("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
