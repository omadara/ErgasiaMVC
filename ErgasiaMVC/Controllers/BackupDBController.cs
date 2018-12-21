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
    public class BackupDBController : Controller
    {
        // GET: BackupDB
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadBackupFile()
        {
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
    }
}