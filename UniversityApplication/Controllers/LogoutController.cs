using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            if (Session["emailAddress"] != null)
                Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}