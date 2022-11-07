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

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDAL _dal;
        public AccountRepository(IDAL dal)
        {
            _dal = dal;
        }

        public int Insert(UserAccount account)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.Connection;
            int numberOfRowsAffected;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO UserAccount(EmailAddress, HashedPassword, UserAccountStatus, RoleID) VALUES(@EmailAddress, @HashedPassword, @UserAccountStatus, @RoleID)", conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@EmailAddress", account.EmailAddress);
                command.Parameters.AddWithValue("@HashedPassword", account.HashedPassword);
                command.Parameters.AddWithValue("@UserAccountStatus", account.UserAccountStatus);
                command.Parameters.AddWithValue("@RoleID", account.RoleID);
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
            return numberOfRowsAffected;
        }
        public UserAccount getAccountByEmailAddress(string emailAddress)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.Connection;
            UserAccount account = new UserAccount();
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT UserAccountID, EmailAddress, HashedPassword, UserAccountStatus, RoleID FROM UserAccount WHERE EmailAddress=@emailAddress", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@emailAddress", emailAddress);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        account.UserAccountID = (int)reader["UserAccountID"];
                        account.EmailAddress = reader["EmailAddress"].ToString();
                        account.HashedPassword = (byte[])reader["HashedPassword"];
                        account.UserAccountStatus = reader["UserAccountStatus"].ToString();
                        account.RoleID = Convert.ToInt32(reader["RoleID"]);
                    }
                }
            }
            _dal.CloseConnection();
            return account;
        }
        public bool IsValidUser(string emailAddress, byte[] hashedPassword)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.Connection;
            bool isValid = false;
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT EmailAddress FROM UserAccount WHERE EmailAddress=@emailAddress AND HashedPassword = @hashedPassword", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@emailAddress", emailAddress));
                command.Parameters.Add(new SqlParameter("@hashedPassword", hashedPassword));
                SqlDataReader reader = command.ExecuteReader();
                isValid = reader.HasRows;
            }
            _dal.CloseConnection();
            return isValid;
        }
    }
}
