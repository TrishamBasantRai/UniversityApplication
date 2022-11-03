using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    internal interface IStudentService
    {
        List<ValidationResult> StudentValidate(StudentModel student);
    }
}
