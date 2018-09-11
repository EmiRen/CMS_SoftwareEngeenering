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
using System.IO;

namespace CMS4.Controllers
{
    public class PapersController : Controller
    {
        private CMS4Entities db = new CMS4Entities();

        // GET: Papers
        public ActionResult Index()
        {
            var papers = db.Papers.Include(p => p.Authors).Include(p => p.Conferences).Include(p => p.Expertises);
            return View(papers.ToList());
        }

        public ActionResult IndexUserNames()
        {
            //return View(db.Artiles.ToList());
            string currentUserId = User.Identity.GetUserId();
            return View(db.Papers.Where(m => m.AuthorId == currentUserId).ToList());
        }

        // GET: Papers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Papers papers = db.Papers.Find(id);
            if (papers == null)
            {
                return HttpNotFound();
            }
            return View(papers);
        }

        // GET: Papers/Create
        [Authorize(Roles = "Author")]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Id");
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "ChairId");
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise");
            return View();
        }

        // POST: Papers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Author")]
        public ActionResult Create([Bind(Include = "Id,Title,FileUrl,AuthorId,CreateDate,Status,Type,ConferenceId,KeywordId")] Papers papers)
        {
            if (ModelState.IsValid)
            {
                db.Papers.Add(papers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Id", papers.AuthorId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "ConferenceId", papers.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", papers.KeywordId);
            return View(papers);
        }

        // GET: Artiles/CreateIndi
        [Authorize(Roles = "Author")]
        public ActionResult CreateIndividual()
        {
            Papers paper = new Papers();
            string currentUserId = User.Identity.GetUserId();
            paper.AuthorId = currentUserId;
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Title", paper.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", paper.KeywordId);
            return View(paper);
        }


        // POST: Artiles/CreateIndi
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Author")]
        public ActionResult CreateIndividual([Bind(Include = "Id,Title,FileUrl,AuthorId,CreateDate,Status,Type,ConferenceId,KeywordId")] Papers paper)
        {
            if (ModelState.IsValid)
            {
                db.Papers.Add(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Title", paper.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", paper.KeywordId);
            return View(paper);
        }


        /*
        [Authorize(Roles = "Author")]
        public ActionResult Create()
        {
            Papers paper = new Papers();
            string currentUserId = User.Identity.GetUserId();
            paper.AuthorId = currentUserId;
            return View(paper);
        }
        // POST: Artiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Chair")]
        public ActionResult Create([Bind(Include = "Id,Title,FileUrl,AuthorId,CreateDate,Status,Type,ConferenceId,KeywordId")] Papers paper)
        {
            if (ModelState.IsValid)
            {
                db.Papers.Add(paper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Id", paper.AuthorId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "ConferenceId", paper.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", paper.KeywordId);
      
            return View(paper);
        } */


        // GET: Papers/Edit/5
        [Authorize(Roles = "Author, Chair")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Papers papers = db.Papers.Find(id);
            if (papers == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Id", papers.AuthorId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Title", papers.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", papers.KeywordId);
            return View(papers);
        }

        // POST: Papers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Author, Chair")]
        public ActionResult Edit([Bind(Include = "Id,Title,FileUrl,AuthorId,CreateDate,Status,Type,ConferenceId,KeywordId")] Papers papers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(papers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Id", papers.AuthorId);
            ViewBag.ConferenceId = new SelectList(db.Conferences, "Id", "Title", papers.ConferenceId);
            ViewBag.KeywordId = new SelectList(db.Expertises, "ExpertiseId", "Expertise", papers.KeywordId);
            return View(papers);
        }

        // GET: Papers/Delete/5
        [Authorize(Roles = "Author, Chair")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Papers papers = db.Papers.Find(id);
            if (papers == null)
            {
                return HttpNotFound();
            }
            return View(papers);
        }

        // POST: Papers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Author, Chair")]
        public ActionResult DeleteConfirmed(int id)
        {
            Papers papers = db.Papers.Find(id);
            db.Papers.Remove(papers);
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
