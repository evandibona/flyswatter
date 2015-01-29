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
    public class BugUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BugUsers
        public ActionResult Index()
        {
            var bugUsers = db.BugUsers.Include(b => b.Project);
            return View(bugUsers.ToList());
        }

        // GET: BugUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUser bugUser = db.BugUsers.Find(id);
            if (bugUser == null)
            {
                return HttpNotFound();
            }
            return View(bugUser);
        }

        // GET: BugUsers/Create
        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            return View();
        }

        // POST: BugUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserId,FirstName,LastName,UserName")] BugUser bugUser)
        {
            if (ModelState.IsValid)
            {
                db.BugUsers.Add(bugUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", bugUser.ProjectId);
            return View(bugUser);
        }

        // GET: BugUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUser bugUser = db.BugUsers.Find(id);
            if (bugUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", bugUser.ProjectId);
            return View(bugUser);
        }

        // POST: BugUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserId,FirstName,LastName,UserName")] BugUser bugUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bugUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", bugUser.ProjectId);
            return View(bugUser);
        }

        // GET: BugUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUser bugUser = db.BugUsers.Find(id);
            if (bugUser == null)
            {
                return HttpNotFound();
            }
            return View(bugUser);
        }

        // POST: BugUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BugUser bugUser = db.BugUsers.Find(id);
            db.BugUsers.Remove(bugUser);
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
    }
}
