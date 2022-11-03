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

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DAL _dal = new DAL(); //This will change with use of Unity

        public int GetStudentID(string emailAddress)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            int StudentID = 2;
            using (conn)
            {
                SqlCommand command = new SqlCommand("Select StudentID FROM StudentDetails WHERE EmailAddress = @emailAddress", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@EmailAddress", HttpContext.Current.Session["EmailAddress"].ToString());
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StudentID = (int)reader["StudentID"];
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
                SqlCommand command = new SqlCommand("INSERT INTO StudentDetails(NID, FirstName, LastName, Address, PhoneNumber, DateOfBirth, GuardianName, EmailAddress) VALUES(@NID, @FirstName, @LastName, @Address, @PhoneNumber, @DateOfBirth, @GuardianName, @EmailAddress)", conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@NID", studentModel.NationalIdentityNumber);
                command.Parameters.AddWithValue("@FirstName", studentModel.FirstName);
                command.Parameters.AddWithValue("@LastName", studentModel.LastName);
                command.Parameters.AddWithValue("@Address", studentModel.Address);
                command.Parameters.AddWithValue("@PhoneNumber", studentModel.PhoneNumber);
                command.Parameters.AddWithValue("@DateOfBirth", studentModel.DateOfBirth);
                command.Parameters.AddWithValue("@GuardianName", studentModel.GuardianName);
                command.Parameters.AddWithValue("@EmailAddress", HttpContext.Current.Session["EmailAddress"].ToString());
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
            return numberOfRowsAffected;
        }
    }
}
