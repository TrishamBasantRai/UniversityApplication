using BusinesLayer.Services;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        public LoginController(ILoginService loggingIn, IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _loggingIn = loggingIn;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LogIntoAccount(string emailAddress, string password)
        {
            List<ValidationResult> listOfErrors = _loggingIn.LoginResults(emailAddress, password);
            UserAccount loginAccount = _accountRepository.getAccountByEmailAddress(emailAddress);
            int loginAccountID = loginAccount.UserAccountID;
            string accountRole = "Student";
            if (!listOfErrors.Any())
            {
                Session["emailAddress"] = emailAddress;
                Session["userAccountID"] = loginAccountID;
                accountRole = _roleRepository.GetRoleNameByEmailAddress(emailAddress);
            }
            return Json(new { data = listOfErrors, hasErrors = listOfErrors.Any(), url = Url.Action("Index", accountRole) });
        }
    }
}