using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Common
{
    public interface IDatabaseCommand
    {
        DataTable GetData(string query);
        bool InsertUpdateData(string query, List<SqlParameter> parameters);
        DataTable GetDataWithConditions(string query, List<SqlParameter> parameters);
    }
}
