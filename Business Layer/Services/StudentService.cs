using Business_Layer.Functions;
using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
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
        private readonly IValidateStudent _validateStudent;
        private readonly IStudentRepository _studentRepository;
        public StudentService(IValidateStudent validateStudent, IStudentRepository studentRepository)
        {
            _validateStudent = validateStudent;
            _studentRepository = studentRepository;
        }

        public List<ValidationResult> StudentValidate(StudentModel studentModel)
        {
            //ValidateStudent validateStudent = new ValidateStudent();
            List<ValidationResult> results = _validateStudent.StudentValidation(studentModel);
            //StudentRepository studentRepository = new StudentRepository();
            if(results.Count == 0)
                _studentRepository.Insert(studentModel);
            return results;
        }
    }
}
