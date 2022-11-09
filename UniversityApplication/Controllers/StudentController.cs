using BusinesLayer.Services;
using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IRedirectService _redirectService;
        private readonly IApplicationService _applicationService;
        public StudentController(IStudentService studentService, IRedirectService redirectService, IApplicationService applicationService)
        {
            _studentService = studentService;
            _redirectService = redirectService;
            _applicationService = applicationService;
        }
        public ActionResult Index()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            if(_redirectService.HasAlreadyInputDetails())
                return RedirectToAction("InputResults", "Student");
            return View();
        }
        [HttpPost]
        public JsonResult InputDetails(StudentModel studentModel)
        {
            List<ValidationResult> listOfErrorsOfInput = _studentService.StudentValidate(studentModel);
            return Json(new { data = listOfErrorsOfInput, hasErrors = listOfErrorsOfInput.Any(), url = Url.Action("InputResults", "Student") });
        }
        public ActionResult InputResults()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            if(_redirectService.HasAlreadyInputResults())
                return RedirectToAction("StudentApplicationStatus", "Student");
            return View();
        }
        public ActionResult StudentApplicationStatus()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        [HttpPost]
        public JsonResult GetStatusDetails()
        {
            ApplicationStatusModel studentStatusDetails = _applicationService.GetApplicationStatusDetails();
            return Json(new { data = studentStatusDetails });
        }
    }
}