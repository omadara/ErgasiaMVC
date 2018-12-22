using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ErgasiaMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index() {
            if (Session["is_admin"] != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginPost(string username, string password) {
            if (username == "admin" && password == "pass") {
                Session["is_admin"] = true;
                RouteValueDictionary redirectUrl = (RouteValueDictionary)Session["redirect_url"];
                if(redirectUrl!=null) return RedirectToAction(redirectUrl["action"].ToString(), redirectUrl["controller"].ToString());
                else return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        public static bool shouldRedirectToLogin(Controller c) {
            if (c.Session["is_admin"] == null) {
                c.Session["redirect_url"] = c.ControllerContext.RouteData.Values;
                return true;
            }
            return false;
        }
    }
}