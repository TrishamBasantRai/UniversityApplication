using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Common
{
    public class DatabaseCommand : IDatabaseCommand
    {
        private readonly IDAL _dal;
        public DatabaseCommand(IDAL dal)
        {
            _dal = dal;
        }
        public DataTable GetData(string query)
        {
            _dal.OpenConnection();
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dal.Connection))
            {
                command.CommandType = CommandType.Text;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            _dal.CloseConnection();

            return dataTable;
        }

        public bool InsertUpdateData(string query, List<SqlParameter> parameters)
        {
            _dal.OpenConnection();
            int numberOfRowsAffected = 0;
            using (SqlCommand command = new SqlCommand(query, _dal.Connection))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
           return (numberOfRowsAffected > 0);
        }

        public DataTable GetDataWithConditions(string query, List<SqlParameter> parameters)
        {
            _dal.OpenConnection();
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _dal.Connection))
            {
                command.CommandType = CommandType.Text;
                if (parameters != null)
                {
                    parameters.ForEach(parameter => {
                        command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
                    });
                }
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            _dal.CloseConnection();

            return dataTable;
        }
    }
}
