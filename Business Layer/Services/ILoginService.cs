﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Services
{
    public interface ILoginService
    {
        List<ValidationResult> LoginResults(string username, string password);
    }
}
