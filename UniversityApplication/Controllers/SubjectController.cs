using Business_Layer.Services;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        [HttpPost]
        public JsonResult GetListOfSubjects()
        {
            List<string> result = _subjectService.GetSubjectList();
            return Json(new { data = result });
        }
    }
}