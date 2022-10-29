using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services
{
    internal interface ILoginService
    {
        bool IsLoggedIn(string username, string password);
    }
}
