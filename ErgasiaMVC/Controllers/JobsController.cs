﻿using System;
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
    public class JobsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: Jobs
        public ActionResult Index()
        {
            return View(db.jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] job job)
        {
            if (ModelState.IsValid)
            {
                db.jobs.Add(job);
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Successful";
                return RedirectToAction("Index");
            }
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] job job)
        {
            if (ModelState.IsValid)
            {
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message_css"] = "alert alert-success";
                TempData["message"] = "Successful";
                return RedirectToAction("Index");
            }
            TempData["message_css"] = "alert alert-danger";
            TempData["message"] = "Invalid input";
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            job job = db.jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            job job = db.jobs.Find(id);
            db.jobs.Remove(job);
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
