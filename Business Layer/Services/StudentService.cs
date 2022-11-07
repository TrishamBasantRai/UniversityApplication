using Business_Layer.Functions;
using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class StudentService : IStudentService
    {
        public List<ValidationResult> StudentValidate(StudentModel studentModel)
        {
            ValidateStudent validateStudent = new ValidateStudent();
            List<ValidationResult> results = validateStudent.StudentValidation(studentModel);
            StudentRepository studentRepository = new StudentRepository();
            if(results.Count == 0)
                studentRepository.Insert(studentModel);
            return results;
        }
    }
}
