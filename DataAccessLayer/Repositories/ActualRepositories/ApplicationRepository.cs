using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Common;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DAL _dal = new DAL();
        public bool Insert(ApplicationModel applicationModel)
        {
            int numberOfRowsAffected;
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Studentapplication(StudentResultScore, StudentApplicationStatus, StudentID) VALUES(@StudentResultScore, @StudentApplicationStatus, @StudentID)", conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@StudentResultScore", applicationModel.StudentResultScore);
                command.Parameters.AddWithValue("@StudentApplicationStatus", applicationModel.ApplicationStatus);
                command.Parameters.AddWithValue("@StudentID", applicationModel.StudentID);
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
            return (numberOfRowsAffected>0);
        }
    }
}
