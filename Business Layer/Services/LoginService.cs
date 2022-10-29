using Business_Layer.Security;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class LoginService : ILoginService
    {
        public bool IsLoggedIn(string emailAddress, string password)
        {
            AccountRepository accountRepository = new AccountRepository();
            UserAccount accountToBeCrossCheckedWith = accountRepository.getAccountByEmailAddress(emailAddress);
            if (accountToBeCrossCheckedWith.EmailAddress == null)
                return false;
            byte[] hashedpass = PasswordHashing.ComputeStringToSha256Hash(password);
            if (accountToBeCrossCheckedWith.HashedPassword.Length != hashedpass.Length)
                return false;
            for (int i = 0; i < hashedpass.Length; i++)
            {
                if (hashedpass[i] != accountToBeCrossCheckedWith.HashedPassword[i])
                    return false;
            }
            return true;
        }
    }
}
