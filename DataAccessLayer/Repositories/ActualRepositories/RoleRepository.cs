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
                SqlCommand command = new SqlCommand("SELECT * FROM UserRole WHERE RoleName=@RoleName", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@RoleName", roleName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        role.RoleID = (int)reader["RoleID"];
                        role.RoleName = reader["RoleName"].ToString();
                    }
                }
            }
            _dal.CloseConnection();
            return role.RoleID;
        }
    }
}
