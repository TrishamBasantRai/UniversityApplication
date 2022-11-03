using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult Index()
        {
            SubjectRepository subjectRepository = new SubjectRepository();
            List<string> result = subjectRepository.getSubjectList();
            return Json(new { data = result });
        }
    }
}