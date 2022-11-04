using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["emailAddress"] == null)
                return RedirectToAction("Index", "Login");
            return View();
        }

        [HttpPost]
        public JsonResult GetSummary()
        {
            StudentSummaryRepository studentSummaryRepository = new StudentSummaryRepository();
            List<StudentSummaryModel> result = studentSummaryRepository.GetStudentSummary();
            return Json(new { data = result });
        }
    }
}