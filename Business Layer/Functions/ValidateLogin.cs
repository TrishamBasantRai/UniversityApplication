using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    public class ValidateLogin : IValidateLogin
    {
        public List<ValidationResult> ValidationLoginResults(string emailAddress, string password)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            Regex EmailRegex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
            if (string.IsNullOrEmpty(emailAddress))
                results.Add(new ValidationResult("Please enter an email address."));
            if (string.IsNullOrEmpty(password))
                results.Add(new ValidationResult("Please enter a password."));
            if ((!string.IsNullOrEmpty(emailAddress)) && (!EmailRegex.IsMatch(emailAddress)))
                results.Add(new ValidationResult("Invalid Email Address!"));
            return results;
        }
    }
}
