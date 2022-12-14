using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IRoleRepository
    {
        int GetRoleIDByRoleName(string roleName);
        string GetRoleNameByEmailAddress(string emailAddress);
    }
}
