using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccessLayer.Common;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly DAL _dal = new DAL();
        public bool Insert(ResultModel resultModel)
        {
            StudentRepository studentRepository = new StudentRepository();
            int StudentID = studentRepository.GetStudentID();
            SubjectRepository subjectRepository = new SubjectRepository();
            int numberOfRowsAffected;
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO StudentResult(SubjectID, Grade, StudentID) VALUES(@SubjectID, @Grade, @StudentID)", conn);
                command.CommandType = CommandType.Text;

                for (int i = 0; i < resultModel.SubjectNames.Count; i++)
                {
                    command.Parameters.AddWithValue("@SubjectID", subjectRepository.GetSubjectID(resultModel.SubjectNames[i]));
                    command.Parameters.AddWithValue("@Grade", resultModel.Grades[i]);
                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    numberOfRowsAffected = command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
            _dal.CloseConnection();
            return true;
        }
        public bool ResultExists(int studentID)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT TOP 1 StudentID FROM StudentResult WHERE StudentID=@StudentID", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@StudentID", studentID);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    return true;
            }
            _dal.CloseConnection();
            return false;
        }
    }
}
