using Business_Layer.Functions;
using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinesLayer.Functions
{
    public class ValidateRegister : IValidateRegister
    {
        private readonly IAccountRepository _accountRepository;

        public ValidateRegister(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<ValidationResult> RegistrationValidation(string emailAddress, string password, string confirmPassword)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            UserAccount accountToBeCrossedCheckedWith = _accountRepository.GetAccountByEmailAddress(emailAddress);
            Regex EmailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            if (string.IsNullOrEmpty(emailAddress))
                results.Add(new ValidationResult("Please enter an email address."));
            if (string.IsNullOrEmpty(password))
                results.Add(new ValidationResult("Please enter a password"));
            if (string.IsNullOrEmpty(confirmPassword))
                results.Add(new ValidationResult("Please confirm your password."));
            if ((password != null) && (confirmPassword != null) && (!password.Equals("")) && (!confirmPassword.Equals("")) && (password != confirmPassword))
                results.Add(new ValidationResult("The passwords you entered do not match."));
            if (!EmailRegex.IsMatch(emailAddress))
                results.Add(new ValidationResult("Invalid Email Address!"));
            else
            {
                if (accountToBeCrossedCheckedWith.EmailAddress == emailAddress)
                    results.Add(new ValidationResult("Email Address already exists."));
            }
            return results;
        }
    }
}  