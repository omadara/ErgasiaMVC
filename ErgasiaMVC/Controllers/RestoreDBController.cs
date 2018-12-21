using ErgasiaMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

                string preq = "ALTER DATABASE pubs SET Single_User WITH Rollback Immediate";
                string reqstq = "Restore database pubs from disk='" + path + "'";
                string postq = "ALTER DATABASE pubs SET Multi_User";

                SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["masterCS"].ConnectionString);
                sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand(preq, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCmd = new SqlCommand(reqstq, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCmd = new SqlCommand(postq, sqlCon);
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

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