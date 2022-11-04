using BusinesLayer.Services;
using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
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
        public ActionResult Index()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            StudentRepository studentRepository = new StudentRepository();
            int studentID = studentRepository.GetStudentID();
            if (studentID > 0)
                return RedirectToAction("Results", "Student");
            return View();
        }

        [HttpPost]
        public JsonResult Index(StudentModel studentModel)
        {
            StudentService newStudentService = new StudentService();
            List<ValidationResult> result = newStudentService.StudentValidate(studentModel);
            return Json(new { data = result, hasErrors = result.Any(), url = Url.Action("Results", "Student") });
        }

        public ActionResult Results()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            StudentRepository studentRepository = new StudentRepository();
            int studentID = studentRepository.GetStudentID();
            ResultRepository resultRepository = new ResultRepository();
            if (resultRepository.ResultExists(studentID))
                return RedirectToAction("Application", "Student");
            return View();
        }

        [HttpPost]
        public JsonResult GetSubjects()
        {
            SubjectRepository subjectRepository = new SubjectRepository();
            List<string> result = subjectRepository.getSubjectList();
            return Json(new { data = result } );
        }

        public ActionResult Application()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }
    }
}