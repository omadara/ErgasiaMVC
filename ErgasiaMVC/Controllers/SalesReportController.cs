using ErgasiaMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgasiaMVC.Controllers
{
    public class SalesReportController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: SalesReport
        public ActionResult Index() {
            return View();
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult IndexPost([Bind(Include = "startDate,endDate,storeNameStart,storeNameEnd")] SalesReportInput input) {
            if(ModelState.IsValid) {
                ViewBag.salesReportResult = db.sales
                    .Where(s => s.ord_date >= input.startDate && s.ord_date <= input.endDate
                        && s.store.stor_name.ToLower().CompareTo(input.storeNameStart.ToLower()) >= 0 
                        && s.store.stor_name.ToLower().CompareTo(input.storeNameEnd.ToLower()) <= 0)
                    .ToList();
            }
            return View();
        }
    }
}
