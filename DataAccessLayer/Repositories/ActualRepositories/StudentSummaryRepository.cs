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
    public class StudentSummaryRepository : IStudentSummaryRepository
    {
        private readonly IDAL _dal;
        public StudentSummaryRepository(IDAL dal)
        {
            _dal = dal;
        }
        public List<StudentSummaryModel> GetStudentSummary()
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.Connection;
            List<StudentSummaryModel> studentSummaryList = new List<StudentSummaryModel>();
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT NationalIdentityNumber, FirstName, LastName, StudentResultScore, StudentApplicationStatus FROM StudentDetails INNER JOIN StudentApplication ON StudentDetails.StudentID = StudentApplication.StudentID", conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        studentSummaryList.Add(new StudentSummaryModel(){
                            NationalIdentityNumber = reader["NationalIdentityNumber"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            TotalScore = Convert.ToInt32(reader["StudentResultScore"]),
                            ApplicationStatus = reader["StudentApplicationStatus"].ToString()
                        });
                    }
                }
            }
            _dal.CloseConnection();
            return studentSummaryList;
        }
    }
}
