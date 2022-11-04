using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IAccountRepository
    {
        int Insert(UserAccount account);
        UserAccount getAccountByEmailAddress(string emailAddress);
        bool IsValidUser(string emailAddress, byte[] hashedPassword);
    }
}
