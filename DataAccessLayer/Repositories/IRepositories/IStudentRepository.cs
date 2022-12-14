using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IStudentRepository
    {
        bool InsertNewStudentDetails(StudentModel student);
        int GetStudentIDOfCurrentSession();
        bool NationalIdentityNumberAlreadyExists(string nationalIdentityNumber);
        bool PhoneNumberAlreadyExists(string phoneNumber);
    }
}
