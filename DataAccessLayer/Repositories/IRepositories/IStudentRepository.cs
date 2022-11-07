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
        int Insert(StudentModel student);
        int GetStudentID();
        bool NationalIdentityNumberExists(string nationalIdentityNumber);
        bool PhoneNumberExists(string phoneNumber);
    }
}
