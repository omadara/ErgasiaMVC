using ErgasiaMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ErgasiaMVC.Controllers
{
    public class DatabaseController : Controller
    {
        public ActionResult Index() {
            return View();
        }

        public ActionResult DownloadBackupFile() {
            string backup_dir = Path.Combine(Path.GetTempPath(), "mvc_ergasia_db");
            string backup_file = "pubs-" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".Bak";

            Directory.CreateDirectory(backup_dir);

            pubsEntities db = new pubsEntities();
            SqlConnection sqlCon = (SqlConnection)(db.Database.Connection);
            sqlCon.Open();
            SqlCommand sqlCmd = new SqlCommand("backup database pubs to disk='" + Path.Combine(backup_dir, backup_file) + "'", sqlCon);
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            db.Dispose();

            byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(backup_dir, backup_file));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, backup_file);
        }
        

        [HttpPost]
        public ActionResult Restore(HttpPostedFileBase file) {
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
                ViewBag.cssClass = "alert alert-success";
            }else {
                ViewBag.message = "Invalid file";
                ViewBag.cssClass = "alert alert-danger";
            }
            return View("Index");
        }
    }
}
