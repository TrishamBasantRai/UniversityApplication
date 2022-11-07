using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStudentSummaryRepository _studentSummaryRepository;
        public AdminController(IStudentSummaryRepository studentSummaryRepository)
        {
            _studentSummaryRepository = studentSummaryRepository;
        }
        public ActionResult Index()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }
        [HttpPost]
        public JsonResult GetSummaryOfStudentsApplied()
        {
            List<StudentSummaryModel> result = _studentSummaryRepository.GetStudentSummary();
            return Json(new { data = result });
        }
    }
}