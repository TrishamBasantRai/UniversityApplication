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
        private readonly IDatabaseCommand _databaseCommand;
        private const string GetRoleIDByItsCorrespondingName = @"SELECT RoleID, RoleName FROM UserRole WHERE RoleName=@RoleName";
        private const string GetRoleNameByCorrespondingEmailAddress = @"SELECT RoleName FROM UserRole 
                                                                        INNER JOIN UserAccount ON UserRole.RoleID = UserAccount.RoleID 
                                                                        WHERE UserAccount.EmailAddress=@EmailAddress";
        public RoleRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }
        public int GetRoleIDByRoleName(string roleName)
        {
            UserRole role = new UserRole();
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RoleName", roleName));
            var dataTable = _databaseCommand.GetDataWithConditions(GetRoleIDByItsCorrespondingName, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                role.RoleID = Convert.ToInt32(row["RoleID"]);
                role.RoleName = row["RoleName"].ToString();
            }
            return role.RoleID;

        }
        public string GetRoleNameByEmailAddress(string emailAddress)
        {
            string roleName = "";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmailAddress", emailAddress));
            var dataTable = _databaseCommand.GetDataWithConditions(GetRoleNameByCorrespondingEmailAddress, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                roleName = row["RoleName"].ToString();
            }
            return roleName;
        }
    }
}
