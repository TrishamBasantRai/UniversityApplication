using BusinesLayer.Security;
using Business_Layer.Functions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IValidateLogin _loginDataValidation;
        public LoginService(IAccountRepository accountRepository, IValidateLogin loginDataValidation)
        {
            _accountRepository = accountRepository;
            _loginDataValidation = loginDataValidation;
        }
        public List<ValidationResult> LoginResults(string emailAddress, string password)
        {
            List<ValidationResult> results = _loginDataValidation.ValidationLoginResults(emailAddress, password);
            if (results.Count > 0)
                return results;
            if (!_accountRepository.IsValidUser(emailAddress, PasswordHashing.ComputeStringToSha256Hash(password)))
                results.Add(new ValidationResult("Unable to authenticate user!"));
            return results;
        }
    }
}