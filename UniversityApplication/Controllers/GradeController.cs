using Business_Layer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost]
        public JsonResult GetListOfGrades()
        {
            List<GradeDetails> result = _gradeService.GetGradeList();
            return Json(new { data = result });
        }
    }
}