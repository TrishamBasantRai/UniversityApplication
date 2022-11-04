using BusinesLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
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

        public LoginController(ILoginService logginIn)
        {
            _loggingIn = logginIn;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Index(string emailAddress, string password)
        {
            //LoginService loggingIn = new LoginService();
            List<ValidationResult> result = _loggingIn.LoginResults(emailAddress, password);
            AccountRepository accountRepository = new AccountRepository();
            UserAccount loginAccount = accountRepository.getAccountByEmailAddress(emailAddress);
            RoleRepository roleRepository = new RoleRepository();
            string role = "Student";
            int loginAccountID = loginAccount.UserAccountID;
            if (!result.Any())
            {
                Session["emailAddress"] = emailAddress;
                Session["userAccountID"] = loginAccountID;
                role = roleRepository.GetRoleNameByEmailAddress(emailAddress);
            }
            return Json(new { data = result, hasErrors = result.Any(), url = Url.Action("Index", role) });
        }
    }
}