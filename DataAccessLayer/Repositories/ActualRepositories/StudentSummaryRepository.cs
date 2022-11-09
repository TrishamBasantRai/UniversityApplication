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
using DataAccessLayer.Entities;
using System.Net.Mail;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class StudentSummaryRepository : IStudentSummaryRepository
    {
        private readonly IDatabaseCommand _databaseCommand;
        private const string GetStudentApplicationSummary = @"SELECT NationalIdentityNumber, FirstName, LastName, 
                                                              StudentResultScore, StudentApplicationStatus 
                                                              FROM StudentDetails 
                                                              INNER JOIN StudentApplication 
                                                              ON StudentDetails.StudentID = StudentApplication.StudentID";
        public StudentSummaryRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }
        public List<StudentSummaryModel> GetStudentSummary()
        {
            var dataTable = _databaseCommand.GetData(GetStudentApplicationSummary);
            List<StudentSummaryModel> studentSummaryList = new List<StudentSummaryModel>();
            foreach (DataRow row in dataTable.Rows)
            {
                studentSummaryList.Add(new StudentSummaryModel()
                {
                    NationalIdentityNumber = row["NationalIdentityNumber"].ToString(),
                    FirstName = row["FirstName"].ToString(),
                    LastName = row["LastName"].ToString(),
                    TotalScore = Convert.ToInt32(row["StudentResultScore"]),
                    ApplicationStatus = row["StudentApplicationStatus"].ToString()
                });
            }
            return studentSummaryList;
        }
    }
}
