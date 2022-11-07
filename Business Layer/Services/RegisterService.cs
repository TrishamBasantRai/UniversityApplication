using BusinesLayer.Functions;
using BusinesLayer.Security;
using Business_Layer.Functions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRoleRepository _roleRepository;
        //private readonly
        private readonly IValidateRegister _validateRegister;

        public RegisterService(IAccountRepository accountRepository, IRoleRepository roleRepository, IValidateRegister validateRegister)
        {
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
            _validateRegister = validateRegister;
        }

        public List<ValidationResult> RegisterAccount(string emailAddress, string password, string confirmPassword, string roleName)
        {
            //ValidateRegister ValidateInputData = new ValidateRegister(_accountRepository);
            List<ValidationResult> result = _validateRegister.RegistrationValidation(emailAddress, password, confirmPassword);
            //RoleRepository getRoleID = new RoleRepository();
            UserAccount newAccount = new UserAccount();
            string defaultAccountStatus = "Active";
            if (result.Count == 0)
            {
                newAccount.EmailAddress = emailAddress;
                newAccount.HashedPassword = PasswordHashing.ComputeStringToSha256Hash(password);
                newAccount.RoleID = _roleRepository.GetRoleIDByRoleName(roleName);
                newAccount.UserAccountStatus = defaultAccountStatus;
                _accountRepository.Insert(newAccount);
            }
            return result;
        }
    }
}
