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
    public class ChairsController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Chairs
        public ActionResult Index()
        {
            var chairs = db.Chairs.Include(c => c.Expertises).Include(c => c.Users);
            return View(chairs.ToList());
        }

        // GET: Chairs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairs chairs = db.Chairs.Find(id);
            if (chairs == null)
            {
                return HttpNotFound();
            }
            return View(chairs);
        }

        // GET: Chairs/Create
        public ActionResult Create()
        {
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise");
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Chairs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExpertiseId")] Chairs chairs)
        {
            if (ModelState.IsValid)
            {
                db.Chairs.Add(chairs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", chairs.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", chairs.Id);
            return View(chairs);
        }

        // GET: Chairs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairs chairs = db.Chairs.Find(id);
            if (chairs == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", chairs.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", chairs.Id);
            return View(chairs);
        }

        // POST: Chairs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExpertiseId")] Chairs chairs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chairs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExpertiseId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", chairs.ExpertiseId);
            ViewBag.Id = new SelectList(db.Users, "Id", "FirstName", chairs.Id);
            return View(chairs);
        }

        // GET: Chairs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chairs chairs = db.Chairs.Find(id);
            if (chairs == null)
            {
                return HttpNotFound();
            }
            return View(chairs);
        }

        // POST: Chairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Chairs chairs = db.Chairs.Find(id);
            db.Chairs.Remove(chairs);
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
