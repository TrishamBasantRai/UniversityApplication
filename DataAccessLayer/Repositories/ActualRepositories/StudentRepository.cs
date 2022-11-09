using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using DataAccessLayer.Models.ViewModels;
using System.Web;
using System.Web.Security;
using System.Net.Mail;
using System.Security.Principal;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDatabaseCommand _databaseCommand;
        private const string GetCurrentUserStudentID = @"Select StudentID FROM StudentDetails 
                                                         WHERE UserAccountID = @UserAccountID";
        private const string InsertNewStudent = @"INSERT INTO StudentDetails(NationalIdentityNumber, FirstName, 
                                                  LastName, Address, PhoneNumber, DateOfBirth, GuardianName, UserAccountID) 
                                                  VALUES(@NationalIdentityNumber, @FirstName, 
                                                  @LastName, @Address, @PhoneNumber, @DateOfBirth, @GuardianName, @UserAccountID)";
        private const string CheckIfNationalIdentityNumberAlreadyExists = @"SELECT StudentID FROM StudentDetails 
                                                                            WHERE NationalIdentityNumber=@NationalIdentityNumber";
        private const string CheckIfPhoneNumberAlreadyExists = @"SELECT StudentID FROM StudentDetails WHERE PhoneNumber=@PhoneNumber";
        public StudentRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }

        public int GetStudentIDOfCurrentSession()
        {
            int studentIDDoesNotExist = -1;
            int studentID = studentIDDoesNotExist;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserAccountID", HttpContext.Current.Session["UserAccountID"]));
            var dataTable = _databaseCommand.GetDataWithConditions(GetCurrentUserStudentID, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                studentID = Convert.ToInt32(row["StudentID"]);
            }
            return studentID;
        }

        public bool InsertNewStudentDetails(StudentModel studentModel)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@NationalIdentityNumber", studentModel.NationalIdentityNumber));
                parameters.Add(new SqlParameter("@FirstName", studentModel.FirstName));
                parameters.Add(new SqlParameter("@LastName", studentModel.LastName));
                parameters.Add(new SqlParameter("@Address", studentModel.Address));
                parameters.Add(new SqlParameter("@PhoneNumber", studentModel.PhoneNumber));
                parameters.Add(new SqlParameter("@DateOfBirth", studentModel.DateOfBirth));
                if (string.IsNullOrEmpty(studentModel.GuardianName))
                    studentModel.GuardianName = "";
                parameters.Add(new SqlParameter("@GuardianName", studentModel.GuardianName));
                parameters.Add(new SqlParameter("@UserAccountID", HttpContext.Current.Session["userAccountID"]));
                return _databaseCommand.InsertUpdateData(InsertNewStudent, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool NationalIdentityNumberAlreadyExists(string nationalIdentityNumber)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@NationalIdentityNumber", nationalIdentityNumber));
            var dataTable = _databaseCommand.GetDataWithConditions(CheckIfNationalIdentityNumberAlreadyExists, parameters);
            return (dataTable.Rows.Count > 0);
        }

        public bool PhoneNumberAlreadyExists(string phoneNumber)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
            var dataTable = _databaseCommand.GetDataWithConditions(CheckIfPhoneNumberAlreadyExists, parameters);
            return (dataTable.Rows.Count > 0);
        }
    }
}
