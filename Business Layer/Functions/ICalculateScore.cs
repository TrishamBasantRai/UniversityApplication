﻿using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Functions
{
    internal interface ICalculateScore
    {
        int CalculateTotalScore(ResultModel resultModel);
    }
}