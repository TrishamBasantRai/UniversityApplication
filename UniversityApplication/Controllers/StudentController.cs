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
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentService _studentService;
        private readonly IResultRepository _resultRepository;
        public StudentController(IStudentRepository studentRepository, IStudentService studentService, IResultRepository resultRepository)
        {
            _studentRepository = studentRepository;
            _studentService = studentService;
            _resultRepository = resultRepository;
        }
        public ActionResult Index()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            int studentID = _studentRepository.GetStudentID();
            int invalidStudentID = -1;
            if (studentID != invalidStudentID)
                return RedirectToAction("InputResults", "Student");
            return View();
        }
        [HttpPost]
        public JsonResult InputDetails(StudentModel studentModel)
        {
            List<ValidationResult> listOfErrors = _studentService.StudentValidate(studentModel);
            return Json(new { data = listOfErrors, hasErrors = listOfErrors.Any(), url = Url.Action("InputResults", "Student") });
        }
        public ActionResult InputResults()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            int studentID = _studentRepository.GetStudentID();
            if (_resultRepository.ResultExists(studentID))
                return RedirectToAction("StudentApplicationStatus", "Student");
            return View();
        }
        public ActionResult StudentApplicationStatus()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }
    }
}