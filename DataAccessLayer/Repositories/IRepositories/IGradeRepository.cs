﻿using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    internal interface IGradeRepository
    {
        List<GradeDetails> GetGradeDetails();
    }
}
