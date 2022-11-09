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
        private readonly IRegisterService _registering;
        public RegisterController(IRegisterService registering)
        {
            _registering = registering;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RegisterNewAccount(string emailAddress, string password, string confirmPassword)
        {
            string defaultAccountRole = "Student";
            List<ValidationResult> listOfErrorsOfInput = _registering.RegisterAccount(emailAddress, password, confirmPassword, defaultAccountRole);
            return Json(new { data = listOfErrorsOfInput, hasErrors = listOfErrorsOfInput.Any(), url = Url.Action("Index", "Login") });
        }
    }
}