using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DAL _dal = new DAL(); //This will change with use of Unity
        public int GetRoleIDByRoleName(string roleName)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            UserRole role = new UserRole();
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT RoleID, RoleName FROM UserRole WHERE RoleName=@RoleName", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@RoleName", roleName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role.RoleID = Convert.ToInt32(reader["RoleID"]);
                        role.RoleName = reader["RoleName"].ToString();
                    }
                }
            }
            _dal.CloseConnection();
            return role.RoleID;
        }
        public string GetRoleNameByEmailAddress(string emailAddress)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            UserRole role = new UserRole();
            string roleName = "";
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT RoleName FROM UserRole INNER JOIN UserAccount ON UserRole.RoleID = UserAccount.RoleID WHERE UserAccount.EmailAddress=@EmailAddress", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@EmailAddress", emailAddress);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roleName = reader["RoleName"].ToString();
                    }
                }
            }
            _dal.CloseConnection();
            return roleName;
        }
    }
}
