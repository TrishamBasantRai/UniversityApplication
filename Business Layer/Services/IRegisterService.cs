using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services
{
    public interface IRegisterService
    {
        List<ValidationResult> RegisterAccount(string emailAddress, string password, string confirmPassword, string roleName);
        //List<ValidationResult> RegistrationValidation(string emailAddress, string password, string confirmPassword);
    }
}
