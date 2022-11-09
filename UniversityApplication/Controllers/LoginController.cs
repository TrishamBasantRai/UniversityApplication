using BusinesLayer.Services;
using Business_Layer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace UniversityApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loggingIn;
        private readonly IRoleService _roleService;
        private readonly IAccountService _accountService;
        public LoginController(ILoginService loggingIn, IRoleService roleService, IAccountService accountService)
        {
            _loggingIn = loggingIn;
            _roleService = roleService;
            _accountService = accountService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LogIntoAccount(string emailAddress, string password)
        {
            List<ValidationResult> listOfErrorsOfInput = _loggingIn.LoginResults(emailAddress, password);
            string defaultAccountRole = "Student";
            string accountRole = defaultAccountRole;
            if (!listOfErrorsOfInput.Any())
            {
                Session["emailAddress"] = emailAddress;
                Session["userAccountID"] = _accountService.GetLoginAccountID(emailAddress);
                accountRole = _roleService.GetRoleNameUsingEmailAddress(emailAddress);
            }
            return Json(new { data = listOfErrorsOfInput, hasErrors = listOfErrorsOfInput.Any(), url = Url.Action("Index", accountRole) });
        }
    }
}