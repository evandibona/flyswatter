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
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/Index
        [Authorize]
        public ActionResult Index()
        {
            var users = db.Users; 
            return View(users);
        }

        // GET: Users/Roles
        [Authorize(Roles="Admin")]
        public ActionResult Roles()
        {
            var users = new List<UserViewModel>();
            // Iterating over the roles causes an exception. 
            foreach (var u in db.Users)
            {
                users.Add(new UserViewModel() { Id = u.Id, Roles = new Dictionary<string, bool>(), Email = u.Email });
            }
            foreach (var u in users)
            {
                foreach (var r in db.Roles)
                {
                    var h = new ManageHelpers();
                    var rolename = r.Name;
                    var userId = u.Id;
                    u.Roles.Add(rolename, h.UserIsInRole(userId, rolename));
                }
            }
            return View(users);
        }

        // POST: Users/Roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public ActionResult Roles(List<UserViewModel> model)
        {
            foreach (var user in model)
            {
                foreach (var role in user.Roles)
                {
                    var h = new ManageHelpers();
                    bool roleIsEnabled = h.UserIsInRole(user.Id, role.Key);
                    if (roleIsEnabled != role.Value)
                    {
                        if (role.Value)
                        {
                            h.AddToRole(user.Id, role.Key); 
                        }
                        else
                        {
                            h.RemoveFromRole(user.Id, role.Key); 
                        }
                    }
                }
            }
            return RedirectToAction("Roles");
        }

        // GET: Users/Details/5
        public ActionResult Details(string email)
        {
                if (email == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            ApplicationUser user = db.Users.First(u => u.Email == email); 
                if (user == null)
                {
                    return HttpNotFound();
                }
            var uid = user.Id;
            var aTickets = db.Tickets.Count(t => t.AssignedUserId == uid); 
            var oTickets = db.Tickets.Count(t => t.OwnerUserId == uid);
            var tcount = aTickets + oTickets; 
            var userview = new UserDetailsView()
                    { Id = user.Id, TicketCount = tcount, 
                    FirstName = user.FirstName, LastName = user.LastName, Email = user.Email }; 
            return View(userview);
        }

        // GET: Users/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null) return HttpNotFound();

            return View(applicationUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        /*
        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
    }
}
