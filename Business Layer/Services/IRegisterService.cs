﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    internal interface IRegisterService
    {
        List<ValidationResult> RegisterAccount(string emailAddress, string password, string confirmPassword, string roleName);
    }
}