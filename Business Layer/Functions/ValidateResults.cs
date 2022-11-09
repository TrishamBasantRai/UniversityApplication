using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    public class ValidateResults : IValidateResults
    {
        public List<ValidationResult> ResultsValidation(ResultModel resultModel)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            List<string> subjectNames = resultModel.SubjectNames;
            if (subjectNames.Distinct().Count() != subjectNames.Count)
                results.Add(new ValidationResult("You cannot choose the same subject more than once!"));
            return results;
        }
    }
}
