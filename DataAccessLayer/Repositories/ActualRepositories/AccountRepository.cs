using DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System.Data.Common;
using System.Reflection;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseCommand _databaseCommand;
        private const string InsertNewAccount = @"INSERT INTO UserAccount(EmailAddress, HashedPassword, UserAccountStatus, RoleID) 
                                                  VALUES(@EmailAddress, @HashedPassword, @UserAccountStatus, @RoleID)";
        private const string SelectAccountUsingEmailAddress = @"SELECT UserAccountID, EmailAddress, HashedPassword, UserAccountStatus, RoleID 
                                                                FROM UserAccount WHERE EmailAddress=@emailAddress";
        private const string AuthenticateUserAccount = @"SELECT EmailAddress 
                                                         FROM UserAccount WHERE EmailAddress=@emailAddress AND HashedPassword = @hashedPassword";
        public AccountRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }

        public bool InsertedNewAccount(UserAccount newAccount)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@EmailAddress", newAccount.EmailAddress));
                parameters.Add(new SqlParameter("@HashedPassword", newAccount.HashedPassword));
                parameters.Add(new SqlParameter("@UserAccountStatus", newAccount.UserAccountStatus));
                parameters.Add(new SqlParameter("@RoleID", newAccount.RoleID));
                return _databaseCommand.InsertUpdateData(InsertNewAccount, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserAccount GetAccountByEmailAddress(string emailAddress)
        {
            UserAccount userAccount = new UserAccount();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", emailAddress));
            var dataTable = _databaseCommand.GetDataWithConditions(SelectAccountUsingEmailAddress, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                userAccount.UserAccountID = Convert.ToInt32(row["UserAccountID"]);
                userAccount.EmailAddress = row["EmailAddress"].ToString();
                userAccount.HashedPassword = (byte[])row["HashedPassword"];
                userAccount.UserAccountStatus = row["UserAccountStatus"].ToString();
                userAccount.RoleID = Convert.ToInt32(row["RoleID"]);
            }
            return userAccount;
        }
        public bool IsValidUser(string emailAddress, byte[] hashedPassword)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@emailAddress", emailAddress));
            parameters.Add(new SqlParameter("@hashedPassword", hashedPassword));
            var dataTable = _databaseCommand.GetDataWithConditions(AuthenticateUserAccount, parameters);
            return (dataTable.Rows.Count > 0);
        }
    }
}
