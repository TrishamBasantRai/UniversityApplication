using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessLayer.Common
{
    public class DAL
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public SqlConnection connection;
        public void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public void CloseConnection()
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}
