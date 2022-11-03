using BusinesLayer.Security;
using Business_Layer.Functions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
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
        public List<ValidationResult> LoginResults(string emailAddress, string password)
        {
            ValidateLogin loginDataValidation = new ValidateLogin();
            List<ValidationResult> results = loginDataValidation.ValidationLoginResults(emailAddress, password);
            AccountRepository accountRepository = new AccountRepository();
            if (results.Count == 0)
            {
                if (!accountRepository.IsValidUser(emailAddress, PasswordHashing.ComputeStringToSha256Hash(password)))
                    results.Add(new ValidationResult("Unable to authenticate user!"));
            }
            return results;
        }
    }
}

//Controller will call LoginService - passing email and password as parameters
//Login Service will first validate the data (if it is of type email and password has not empty spaces)
//If data is not valid, skip the insert part, return the list of errors
//if data is valid, cross check the email address and the password with database
//If it matches, authenticate, else, return error