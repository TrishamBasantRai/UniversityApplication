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
            //GetSubjects();
            return View();
        }

        [HttpPost]
        public JsonResult GetSubjects()
        {
            SubjectRepository subjectRepository = new SubjectRepository();
            List<string> result = subjectRepository.getSubjectList();
            return Json(new { data = result } );
        }
    }
}