using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS4.Models;

namespace CMS4.Controllers
{
    public class UsersInformationController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: UsersInformation
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Admins).Include(u => u.AspNetUsers).Include(u => u.Authors).Include(u => u.Chairs).Include(u => u.Reviewers);
            return View(users.ToList());
        }

        // GET: UsersInformation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: UsersInformation/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Admins, "Id", "Id");
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id = new SelectList(db.Authors, "Id", "Id");
            ViewBag.Id = new SelectList(db.Chairs, "Id", "Id");
            ViewBag.Id = new SelectList(db.Reviewers, "Id", "Id");
            return View();
        }

        // POST: UsersInformation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Orgnisation,ImgUrl,PhoneNumber")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Admins, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", users.Id);
            ViewBag.Id = new SelectList(db.Authors, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Chairs, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Reviewers, "Id", "Id", users.Id);
            return View(users);
        }

        // GET: UsersInformation/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Admins, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", users.Id);
            ViewBag.Id = new SelectList(db.Authors, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Chairs, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Reviewers, "Id", "Id", users.Id);
            return View(users);
        }

        // POST: UsersInformation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Orgnisation,ImgUrl,PhoneNumber")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Admins, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", users.Id);
            ViewBag.Id = new SelectList(db.Authors, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Chairs, "Id", "Id", users.Id);
            ViewBag.Id = new SelectList(db.Reviewers, "Id", "Id", users.Id);
            return View(users);
        }

        // GET: UsersInformation/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: UsersInformation/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
