using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
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
    public class ValidateRegister
    {
        public List<ValidationResult> RegistrationValidation(string emailAddress, string password, string confirmPassword)
        {
            List<ValidationResult> results = new List<ValidationResult>(); ;
            return ValidationList(results, emailAddress, password, confirmPassword);
        }
        private static List<ValidationResult> ValidationList(List<ValidationResult> results, string emailAddress, string password, string confirmPassword)
        {
            AccountRepository accountRepository = new AccountRepository();
            UserAccount accountToBeCrossedCheckedWith = accountRepository.getAccountByEmailAddress(emailAddress);
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
                //ValidateDuplicateEmail - Database
                if (accountToBeCrossedCheckedWith.EmailAddress == emailAddress)
                    results.Add(new ValidationResult("Email Address already exists."));
            }
            return results;
        }
    }
}
