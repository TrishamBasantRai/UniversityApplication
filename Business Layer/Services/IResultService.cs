using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public interface IResultService
    {
        bool InsertedResultsAndApplicationPlusUpdatedListOfApplicationStatus(ResultModel resultModel);
        List<ValidationResult> ValidatingResults(ResultModel resultModel);
    }
}
