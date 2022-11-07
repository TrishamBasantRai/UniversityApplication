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
        private readonly IDAL _dal;
        public GradeRepository(IDAL dal)
        {
            _dal = dal;
        }
        public List<GradeDetails> GetGradeDetails()
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.Connection;
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
