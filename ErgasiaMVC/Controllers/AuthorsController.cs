using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ErgasiaMVC.Models;

namespace ErgasiaMVC.Controllers
{
    public class AuthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Authors
        public ActionResult Index()
        {
            return View(db.authors.ToList());
        }

        // GET: Authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Sucessful";
                return RedirectToAction("Index");
            }
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(author);
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Sucessful";
                return RedirectToAction("Index");
            }
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(author);
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id) {
            author author = db.authors.Find(id);
            //foreach (var ta in db.titleauthors.Where(ta => ta.au_id == author.au_id).ToList())
            //    db.titleauthors.Remove(ta);
            db.titleauthors.RemoveRange(author.titleauthors);
            db.authors.Remove(author);
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
