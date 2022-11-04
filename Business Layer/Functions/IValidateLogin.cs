using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    public interface IValidateLogin
    {
        List<ValidationResult> ValidationLoginResults(string emailAddress, string password);
    }
}
