using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    internal interface IStudentRepository
    {
        int Insert(StudentModel student);
        int GetStudentID();
    }
}
