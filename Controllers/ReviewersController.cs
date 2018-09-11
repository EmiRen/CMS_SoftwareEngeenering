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
    public class ReviewersController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Reviewers
        public ActionResult Index()
        {
            var reviewers = db.Reviewers.Include(r => r.Expertises).Include(r => r.Users);
            return View(reviewers.ToList());
        }

        // GET: Reviewers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewers reviewers = db.Reviewers.Find(id);
            if (reviewers == null)
            {
                return HttpNotFound();
            }
            return View(reviewers);
        }

        // GET: Reviewers/Create
        public ActionResult Create()
        {
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise");
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Reviewers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpertiseId")] Reviewers reviewers)
        {
            if (ModelState.IsValid)
            {
                db.Reviewers.Add(reviewers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", reviewers.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", reviewers.Id);
            return View(reviewers);
        }

        // GET: Reviewers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewers reviewers = db.Reviewers.Find(id);
            if (reviewers == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", reviewers.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", reviewers.Id);
            return View(reviewers);
        }

        // POST: Reviewers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpertiseId")] Reviewers reviewers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", reviewers.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", reviewers.Id);
            return View(reviewers);
        }

        // GET: Reviewers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reviewers reviewers = db.Reviewers.Find(id);
            if (reviewers == null)
            {
                return HttpNotFound();
            }
            return View(reviewers);
        }

        // POST: Reviewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Reviewers reviewers = db.Reviewers.Find(id);
            db.Reviewers.Remove(reviewers);
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
