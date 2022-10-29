using BusinesLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string emailAddress, string password, string confirmPassword)
        {
            RegisterService registering = new RegisterService();
            List<ValidationResult> result = registering.RegisterAccount(emailAddress, password, confirmPassword, "Student");
            return Json(new { data = result, hasErrors = result.Any(), url = Url.Action("Index", "Login") });
        }
    }
}