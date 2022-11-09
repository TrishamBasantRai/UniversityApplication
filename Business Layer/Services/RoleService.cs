using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public string GetRoleNameUsingEmailAddress(string emailAddress)
        {
            return _roleRepository.GetRoleNameByEmailAddress(emailAddress); ;
        }
    }
}
