using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }
        [HttpPost]
        public JsonResult InputResults(ResultModel resultModel)
        {
            bool result = _resultService.InputResult(resultModel);
            return Json(new { data = result, url = Url.Action("StudentApplicationStatus", "Student") });
        }
    }
}