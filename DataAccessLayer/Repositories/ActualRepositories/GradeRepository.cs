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
using System.Diagnostics;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly IDatabaseCommand _databaseCommand;
        private const string GetGrades = @"SELECT Grade, GradePoints FROM GradeDetails";
        public GradeRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }
        public List<GradeDetails> GetListOfGradeDetails()
        {
            List<GradeDetails> gradeDetails = new List<GradeDetails>();
            var dataTable = _databaseCommand.GetData(GetGrades);
            foreach (DataRow row in dataTable.Rows)
            {
                gradeDetails.Add(new GradeDetails()
                {
                    Grade = Convert.ToChar(row["Grade"]),
                    GradePoints = Convert.ToByte(row["GradePoints"])
                });
            }
            return gradeDetails;
        }
    }
}
