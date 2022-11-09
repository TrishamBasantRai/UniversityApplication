using Business_Layer.Services;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            List<ValidationResult> listOfErrors = _resultService.ValidatingResults(resultModel);
            bool operationsDoneOnResults = false;
            if(!listOfErrors.Any())
                operationsDoneOnResults = _resultService.InsertedResultsAndApplicationPlusUpdatedListOfApplicationStatus(resultModel);
            return Json(new { data = listOfErrors, operationsCompleted = operationsDoneOnResults, url = Url.Action("StudentApplicationStatus", "Student") });
        }
    }
}