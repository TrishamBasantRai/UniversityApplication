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

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DAL _dal = new DAL(); //This will change with use of Unity
        public int Insert(Student student)
        {
            int numberOfRowsAffected;
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Student(NID, FirstName, LastName, Address, PhoneNumber, DateOfBirth, GuardianName, EmailAddress) VALUES(@NID, @FirstName, @LastName, @Address, @PhoneNumber, @DateOfBirth, @GuardianName, @EmailAddress)", conn);
                command.CommandType = CommandType.Text;

                command.Parameters.AddWithValue("@NID", student.NationalIdentityNumber);
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Address", student.Address);
                command.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                command.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                command.Parameters.AddWithValue("@GuardianName", student.GuardianName);
                command.Parameters.AddWithValue("@EmailAddress", student.EmailAddress);
                numberOfRowsAffected = command.ExecuteNonQuery();
            }
            _dal.CloseConnection();
            return numberOfRowsAffected;
        }
    }
}
