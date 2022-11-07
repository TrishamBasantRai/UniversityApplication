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
        // GET: Result
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult Index(ResultModel resultModel)
        {
            ResultService resultService = new ResultService();
            bool result = resultService.InputResult(resultModel);
            return Json(new { data = result, url = Url.Action("Status", "Student") });
        }
    }
}