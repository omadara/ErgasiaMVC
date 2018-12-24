using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErgasiaMVC.Models;

namespace ErgasiaMVC.Controllers
{
    public class TitlesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Titles
        public ActionResult Index()
        {
            var titles = db.titles.Include(t => t.publisher).Include(t => t.roysched).Include(t=>t.titleauthors);
            return View(titles.ToList());
        }

        // GET: Titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            ViewBag.author_ids = new MultiSelectList(db.authors.Select(a => new { id = a.au_id, au_fulname = a.au_lname + " " + a.au_fname}), "id", "au_fulname");
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title, string[] author_ids) {
            if (ModelState.IsValid) {
                foreach(var aid in author_ids) {
                    titleauthor ta = new titleauthor();
                    ta.au_id = aid;
                    ta.title_id = title.title_id;
                    db.titleauthors.Add(ta);
                }
                db.titles.Add(title);
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Successful";
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            ViewBag.author_ids = new MultiSelectList(db.authors.Select(a => new { id = a.au_id, au_fulname = a.au_lname + " " + a.au_fname }), "id", "au_fulname");
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(title);
        }

        // GET: Titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            ViewBag.author_ids = new MultiSelectList(db.authors.Select(a => new { id = a.au_id, au_fulname = a.au_lname + " " + a.au_fname }), "id", "au_fulname");
            return View(title);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title1,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] title title, string[] author_ids) {
            if (ModelState.IsValid) {
                foreach(var ta in db.titleauthors.Where(ta => ta.title_id == title.title_id).ToList())
                    db.titleauthors.Remove(ta);
                foreach (var aid in author_ids) {
                    titleauthor ta = new titleauthor();
                    ta.au_id = aid;
                    ta.title_id = title.title_id;
                    db.titleauthors.Add(ta);
                }
                db.Entry(title).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Successful";
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", title.pub_id);
            ViewBag.author_ids = new MultiSelectList(db.authors.Select(a => new { id = a.au_id, au_fulname = a.au_lname + " " + a.au_fname }), "id", "au_fulname");
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(title);
        }

        // GET: Titles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            title title = db.titles.Find(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            title title = db.titles.Find(id);
            foreach (var ta in db.titleauthors.Where(ta => ta.title_id == title.title_id).ToList())
                db.titleauthors.Remove(ta);
            db.titles.Remove(title);
            db.SaveChanges();
            TempData["message_css"] = "alert alert-info";
            TempData["message"] = "Deleted";
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
