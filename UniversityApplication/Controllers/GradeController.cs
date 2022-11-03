using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class GradeController : Controller
    {
        // GET: Grade
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpPost]
        public JsonResult Index()
        {
            GradeRepository gradeRepository = new GradeRepository();
            List<GradeDetails> result = gradeRepository.GetGradeDetails();
            return Json(new { data = result });
        }
    }
}