using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgasiaMVC.Controllers
{
    public class RestoreDBController : Controller
    {
        // GET: RestoreDB
        public ActionResult Index() {
            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file) {
            if (file != null && file.ContentLength > 0) {

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Path.GetTempPath(), fileName);
                file.SaveAs(path);
                //...restore db using the file...

                ViewBag.message = "Successful";
                ViewBag.cssClass = "text-success";
            }else {
                ViewBag.message = "Invalid file";
                ViewBag.cssClass = "text-danger";
            }
            return View();
        }
    }
}