using DataAccessLayer.Entities;
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
    public class GradeRepository : IGradeRepository
    {
        DAL _dal = new DAL();
        public List<GradeDetails> GetGradeDetails()
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            List<GradeDetails> gradeDetails = new List<GradeDetails>();
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT Grade, GradePoints FROM GradeDetails", conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //subjectList.Add(new Subject
                        //{
                        //    SubjectName = reader["SubjectName"].ToString()
                        //});
                        gradeDetails.Add(new GradeDetails()
                        {
                            Grade = Convert.ToChar(reader["Grade"]),
                            GradePoints = Convert.ToByte(reader["GradePoints"])
                        });
                    }
                }
            }
            _dal.CloseConnection();
            return gradeDetails;
        }
    }
}
