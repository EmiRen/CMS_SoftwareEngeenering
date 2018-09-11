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
    public class EvaluationsController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Evaluations
        public ActionResult Index()
        {
            var evaluation = db.Evaluation.Include(e => e.Papers).Include(e => e.Reviewers).Include(e => e.Status1);
            return View(evaluation.ToList());
        }

        public ActionResult IndexUserNames()
        {
            //return View(db.Artiles.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Evaluation.Where(m => m.ReviewerId == currentUserId).ToList());
        }

        // GET: Evaluations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluation.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        [Authorize(Roles = "Chair")]
        public ActionResult Create()
        {
            ViewBag.PaperId = new SelectList(db.Papers, "Id", "Title");
            ViewBag.ReviewerId = new SelectList(db.Reviewers, "Id", "Id");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "PaperStatus");
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chair")]
        public ActionResult Create([Bind(Include = "Id,PaperId,ReviewerId,Review1,Rating,Date,StatusId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Evaluation.Add(evaluation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaperId = new SelectList(db.Papers, "Id", "Title", evaluation.PaperId);
            ViewBag.ReviewerId = new SelectList(db.Reviewers, "Id", "Id", evaluation.ReviewerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "PaperStatus", evaluation.StatusId);
            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        [Authorize(Roles = "Chair, Reviewer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluation.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaperId = new SelectList(db.Papers, "Id", "Title", evaluation.PaperId);
            ViewBag.ReviewerId = new SelectList(db.Reviewers, "Id", "Id", evaluation.ReviewerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "PaperStatus", evaluation.StatusId);
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chair, Reviewer")]
        public ActionResult Edit([Bind(Include = "Id,PaperId,ReviewerId,Review1,Rating,Date,StatusId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaperId = new SelectList(db.Papers, "Id", "Title", evaluation.PaperId);
            ViewBag.ReviewerId = new SelectList(db.Reviewers, "Id", "Id", evaluation.ReviewerId);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "PaperStatus", evaluation.StatusId);
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        [Authorize(Roles = "Chair, Reviewer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = db.Evaluation.Find(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [Authorize(Roles = "Chair, Reviewer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluation evaluation = db.Evaluation.Find(id);
            db.Evaluation.Remove(evaluation);
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
