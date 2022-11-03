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

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly DAL _dal = new DAL();
        public bool Insert(ResultModel resultModel)
        {
            StudentRepository studentRepository = new StudentRepository();
            int StudentID = studentRepository.GetStudentID(HttpContext.Current.Session["EmailAddress"].ToString());
            SubjectRepository subjectRepository = new SubjectRepository();
            int numberOfRowsAffected;
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO StudentResult(SubjectID, Grade, StudentID) VALUES(@SubjectID, @Grade, @StudentID)", conn);
                command.CommandType = CommandType.Text;
                for(int i=0; i<resultModel.SubjectNames.Count; i++)
                {
                    command.Parameters.AddWithValue("@SubjectID", subjectRepository.GetSubjectID(resultModel.SubjectNames[i]));
                    command.Parameters.AddWithValue("@Grade", resultModel.Grades[i]);
                    command.Parameters.AddWithValue("@StudentID", StudentID);
                    numberOfRowsAffected = command.ExecuteNonQuery();
                }
            }
            _dal.CloseConnection();
            return true;
        }
    }
}
