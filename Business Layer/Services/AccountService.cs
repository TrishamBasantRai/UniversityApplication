using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public int GetLoginAccountID(string emailAddress)
        {
            UserAccount loginAccount = _accountRepository.GetAccountByEmailAddress(emailAddress);
            return loginAccount.UserAccountID;
        }
    }
}
