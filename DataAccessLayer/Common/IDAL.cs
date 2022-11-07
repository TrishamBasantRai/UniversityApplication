using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Common
{
    public interface IDAL
    {
        SqlConnection Connection { get; }
        void OpenConnection();
        void CloseConnection();
    }
}
