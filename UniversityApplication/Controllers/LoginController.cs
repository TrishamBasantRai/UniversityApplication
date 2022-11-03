using BusinesLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string emailAddress, string password)
        {
            LoginService loggingIn = new LoginService();
            List<ValidationResult> result = loggingIn.LoginResults(emailAddress, password);
            if (!result.Any())
            {
                Session["emailAddress"] = emailAddress;
            }
            return Json(new { data = result, hasErrors = result.Any(), url = Url.Action("Index", "Student") });
        }
    }
}