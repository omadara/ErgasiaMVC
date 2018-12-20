using ErgasiaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgasiaMVC.Controllers
{
    public class TopAuthorsReportController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: TopAuthors
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost()
        {
            int num;
            DateTime startDate, endDate;
            if(Int32.TryParse(Request.Form["num"], out num) && num > 0
                && DateTime.TryParse(Request.Form["startDate"], out startDate)
                && DateTime.TryParse(Request.Form["endDate"], out endDate)) {

                var bestTitles = db.sales.GroupBy(s => s.title_id)
                    .Select(g => new {
                        titleId = g.Key,
                        totalSales = g.Where(s => s.ord_date >= startDate && s.ord_date <= endDate).Sum(s => s.qty) })
                    .OrderByDescending(anon => anon.totalSales)
                    .Take(num);
                ViewBag.bestAuthors = db.authors
                    .Where(a => a.titleauthors
                        .Any(ta => bestTitles
                            .Any(bt => bt.titleId == ta.title_id)))
                    .ToList();
            }
            return View();
        }

    }
}
