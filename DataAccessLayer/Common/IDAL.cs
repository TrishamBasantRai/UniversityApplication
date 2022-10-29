using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Common
{
    internal interface IDAL
    {
        void OpenConnection();
        void CloseConnection();
    }
}
