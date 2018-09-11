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
    public class ExpertisesController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Expertises
        public ActionResult Index()
        {
            return View(db.Expertises.ToList());
        }

        // GET: Expertises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertises expertises = db.Expertises.Find(id);
            if (expertises == null)
            {
                return HttpNotFound();
            }
            return View(expertises);
        }

        // GET: Expertises/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expertises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ExpertiseId,Expertise")] Expertises expertises)
        {
            if (ModelState.IsValid)
            {
                db.Expertises.Add(expertises);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(expertises);
        }

        // GET: Expertises/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertises expertises = db.Expertises.Find(id);
            if (expertises == null)
            {
                return HttpNotFound();
            }
            return View(expertises);
        }

        // POST: Expertises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ExpertiseId,Expertise")] Expertises expertises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expertises).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(expertises);
        }

        // GET: Expertises/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Expertises expertises = db.Expertises.Find(id);
            if (expertises == null)
            {
                return HttpNotFound();
            }
            return View(expertises);
        }

        // POST: Expertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Expertises expertises = db.Expertises.Find(id);
            db.Expertises.Remove(expertises);
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
