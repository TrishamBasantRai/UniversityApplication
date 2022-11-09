using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModels
{
    public class ApplicationStatusModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalPoints { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
