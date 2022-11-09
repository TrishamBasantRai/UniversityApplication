using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IRoleService
    {
        string GetRoleNameUsingEmailAddress(string emailAddress);
    }
}
