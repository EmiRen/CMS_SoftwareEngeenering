using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS4.Models;
using Microsoft.AspNet.Identity;

namespace CMS4.Controllers
{
    public class ConferencesController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Conferences
        public ActionResult Index()
        {
            var conferences = db.Conferences.Include(c => c.Chairs);
            return View(conferences.ToList());
        }

        public ActionResult IndexUserNames()
        {
            //return View(db.Artiles.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Conferences.Where(m => m.ChairId == currentUserId).ToList());
        }

        // GET: Conferences/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conferences conferences = db.Conferences.Find(id);
            if (conferences == null)
            {
                return HttpNotFound();
            }
            return View(conferences);
        }

        // GET: Conferences/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.ChairId = new SelectList(db.Chairs, "Id", "Id");
            return View();
        }

        // POST: Conferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,ChairId,Title,Location,Date,Deadline")] Conferences conferences)
        {
            if (ModelState.IsValid)
            {
                db.Conferences.Add(conferences);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChairId = new SelectList(db.Chairs, "Id", "Id", conferences.ChairId);
            return View(conferences);
        }

        // GET: Conferences/Edit/5
        [Authorize(Roles = "Chair, Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conferences conferences = db.Conferences.Find(id);
            if (conferences == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChairId = new SelectList(db.Chairs, "Id", "Id", conferences.ChairId);
            return View(conferences);
        }

        // POST: Conferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chair, Admin")]
        public ActionResult Edit([Bind(Include = "Id,ChairId,Title,Location,Date,Deadline")] Conferences conferences)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conferences).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChairId = new SelectList(db.Chairs, "Id", "Id", conferences.ChairId);
            return View(conferences);
        }

        // GET: Conferences/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Conferences conferences = db.Conferences.Find(id);
            if (conferences == null)
            {
                return HttpNotFound();
            }
            return View(conferences);
        }

        // POST: Conferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(string id)
        {
            Conferences conferences = db.Conferences.Find(id);
            db.Conferences.Remove(conferences);
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
