﻿using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    public interface IValidateResults
    {
        List<ValidationResult> ResultsValidation(ResultModel resultModel);
    }
}
