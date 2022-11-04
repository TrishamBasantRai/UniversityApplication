﻿using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    public class ValidateStudent
    {
        public List<ValidationResult> StudentValidation(StudentModel student)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            StudentRepository studentRepository = new StudentRepository();
            bool nationalIdentityNumberAlreadyExists = studentRepository.NationalIdentityNumberExists(student.NationalIdentityNumber);
            bool phoneNumberAlreadyExists = studentRepository.PhoneNumberExists(student.PhoneNumber);
            if (nationalIdentityNumberAlreadyExists)
                results.Add(new ValidationResult("There already is an account with this national identity number."));
            if (phoneNumberAlreadyExists)
                results.Add(new ValidationResult("There already is an account with this phone number."));
            if (string.IsNullOrEmpty(student.FirstName))
                results.Add(new ValidationResult("Please enter your first name."));
            if (string.IsNullOrEmpty(student.LastName))
                results.Add(new ValidationResult("Please enter your last name."));
            if (string.IsNullOrEmpty(student.Address))
                results.Add(new ValidationResult("Please enter your address."));
            if (string.IsNullOrEmpty(student.PhoneNumber))
                results.Add(new ValidationResult("Please enter your phone number."));
            if (student.DateOfBirth == null)
                results.Add(new ValidationResult("Please enter your date of birth"));
            if (student.DateOfBirth > DateTime.Now)
                results.Add(new ValidationResult("Please enter a valid date of birth."));
            return results;
        }
    }
}
