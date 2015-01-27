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
        private SwatterEntities db = new SwatterEntities();

        // GET: BugUsers
        public ActionResult Index()
        {
            return View(db.BugUsers.ToList());
        }

        // GET: BugUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUsers bugUsers = db.BugUsers.Find(id);
            if (bugUsers == null)
            {
                return HttpNotFound();
            }
            return View(bugUsers);
        }

        // GET: BugUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BugUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Address,City,userName")] BugUsers bugUsers)
        {
            if (ModelState.IsValid)
            {
                db.BugUsers.Add(bugUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bugUsers);
        }

        // GET: BugUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUsers bugUsers = db.BugUsers.Find(id);
            if (bugUsers == null)
            {
                return HttpNotFound();
            }
            return View(bugUsers);
        }

        // POST: BugUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Address,City,userName")] BugUsers bugUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bugUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bugUsers);
        }

        // GET: BugUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BugUsers bugUsers = db.BugUsers.Find(id);
            if (bugUsers == null)
            {
                return HttpNotFound();
            }
            return View(bugUsers);
        }

        // POST: BugUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BugUsers bugUsers = db.BugUsers.Find(id);
            db.BugUsers.Remove(bugUsers);
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
