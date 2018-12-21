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
        public ActionResult IndexPost([Bind(Include = "num,startDate,endDate")] TopAuthorsReportInput input)
        {
            if(ModelState.IsValid) {
                var bestTitles = db.sales.GroupBy(s => s.title_id)
                    .Select(g => new {
                        titleId = g.Key,
                        totalSales = g.Where(s => s.ord_date >= input.startDate && s.ord_date <= input.endDate).Sum(s => s.qty)
                    })
                    .OrderByDescending(anon => anon.totalSales)
                    .Take(input.num);
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
