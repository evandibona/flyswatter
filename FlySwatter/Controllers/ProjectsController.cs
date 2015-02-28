using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using FlySwatter.Models;

namespace FlySwatter.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create()
        {
            var project = new Project();
            ViewBag.Users = db.Users.ToList();
            return View(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Create(string Name, List<string> Users)
        {
            var projectUsers = Users.Select(u => db.Users.Find(u)).ToList();

            var project = new Project { Name = Name };
            db.Projects.Add(project);


            foreach (var user in projectUsers)
            {
                user.Projects.Add(project);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Projects/Edit/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users = db.Users.ToList();
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Edit(string Name, List<string> SelectedUsers)
        {
            var updateMessage = new string[] { "", "" };
            Project project = db.Projects.First(p => p.Name == Name);
            if (SelectedUsers != null)
            {
                var pusers = project.Users.ToList();
                foreach (var suser in SelectedUsers)
                {
                    bool projectContainsSelected = project.Users.Any(u => u.Id == suser);
                    if (projectContainsSelected)
                    {
                        // Do nothing
                    }
                    if (!projectContainsSelected)
                    {
                        var uToAdd = db.Users.First(u => u.Id == suser);
                        project.Users.Add(uToAdd);
                        updateMessage[0] = "Added to " + project.Name;
                        updateMessage[1] = uToAdd.FirstName + ", \n\n"
                            + "You have been added to the " + project.Name + " project. \n"
                            + "There are currently " + project.Users.Count() + " users assigned. \n"
                            + "Thank You";
                    }
                }
                foreach (var puser in pusers)
                {
                    bool selectedContainsPuser = SelectedUsers.Any(uid => uid == puser.Id);
                    if (selectedContainsPuser)
                    {
                        // Do nothing
                    }
                    if (!selectedContainsPuser)
                    {
                        project.Users.Remove(puser);
                        updateMessage[0] = "Removed from " + project.Name;
                        updateMessage[1] = puser.FirstName + ", \n\n"
                            + "You have been removed from " + project.Name + ". \n"
                            + "Thank You";
                    }
                }
                db.SaveChanges();

                //Send Email
                var fromAddress = new MailAddress("evandibona@gmail.com", "FlySwatter");
                var toAddress = new MailAddress("evandibona@gmail.com", "User");
                string fromPassword = "purple0Pop";
                string subject = updateMessage[0]; 
                string body = updateMessage[1]; 

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            return Redirect("/Projects/Details/" + project.Id.ToString());
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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
