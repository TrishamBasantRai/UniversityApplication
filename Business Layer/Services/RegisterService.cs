using Business_Layer.Functions;
using Business_Layer.Security;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class RegisterService : IRegisterService
    {
        public List<ValidationResult> RegisterAccount(string emailAddress, string password, string confirmPassword, string roleName)
        {
            ValidateRegister ValidateInputData = new ValidateRegister();
            List<ValidationResult> result = ValidateInputData.RegistrationValidation(emailAddress, password, confirmPassword);
            AccountRepository insertAccount = new AccountRepository();
            RoleRepository getRoleID = new RoleRepository();
            UserAccount newAccount = new UserAccount();
            string defaultAccountStatus = "Active";
            if (result.Count == 0)
            {
                newAccount.EmailAddress = emailAddress;
                newAccount.HashedPassword = PasswordHashing.ComputeStringToSha256Hash(password);
                newAccount.RoleID = getRoleID.GetRoleIDByRoleName(roleName);
                newAccount.UserAccountStatus = defaultAccountStatus;
                insertAccount.Insert(newAccount);
            }
            return result;
        }
    }
}
