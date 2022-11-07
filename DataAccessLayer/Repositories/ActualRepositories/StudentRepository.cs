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

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DAL _dal = new DAL(); //This will change with use of Unity

        public int GetStudentID()
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            int invalidStudentID = -1;
            int StudentID = invalidStudentID;
            using (conn)
            {
                SqlCommand command = new SqlCommand("Select StudentID FROM StudentDetails WHERE UserAccountID = @UserAccountID", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@UserAccountID", HttpContext.Current.Session["UserAccountID"]);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]);
                    }
                }
            }
            _dal.CloseConnection();
            return StudentID;
        }

        public int Insert(StudentModel studentModel)
        {
            int numberOfRowsAffected;
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO StudentDetails(NationalIdentityNumber, FirstName, LastName, Address, PhoneNumber, DateOfBirth, GuardianName, UserAccountID) VALUES(@NationalIdentityNumber, @FirstName, @LastName, @Address, @PhoneNumber, @DateOfBirth, @GuardianName, @UserAccountID)", conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@NationalIdentityNumber", studentModel.NationalIdentityNumber);
                command.Parameters.AddWithValue("@FirstName", studentModel.FirstName);
                command.Parameters.AddWithValue("@LastName", studentModel.LastName);
                command.Parameters.AddWithValue("@Address", studentModel.Address);
                command.Parameters.AddWithValue("@PhoneNumber", studentModel.PhoneNumber);
                command.Parameters.AddWithValue("@DateOfBirth", studentModel.DateOfBirth);
                command.Parameters.AddWithValue("@GuardianName", studentModel.GuardianName);
                command.Parameters.AddWithValue("@UserAccountID", HttpContext.Current.Session["userAccountID"].ToString());
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
            return numberOfRowsAffected;
        }

        public bool NationalIdentityNumberExists(string nationalIdentityNumber)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            bool alreadyExists = false;
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT StudentID FROM StudentDetails WHERE NationalIdentityNumber=@NationalIdentityNumber", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@NationalIdentityNumber", nationalIdentityNumber));
                SqlDataReader reader = command.ExecuteReader();
                alreadyExists = reader.HasRows;
            }
            _dal.CloseConnection();
            return alreadyExists;
        }

        public bool PhoneNumberExists(string phoneNumber)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            bool alreadyExists = false;
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT StudentID FROM StudentDetails WHERE PhoneNumber=@PhoneNumber", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@PhoneNumber", phoneNumber));
                SqlDataReader reader = command.ExecuteReader();
                alreadyExists = reader.HasRows;
            }
            _dal.CloseConnection();
            return alreadyExists;
        }
    }
}
