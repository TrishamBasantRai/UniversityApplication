using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModels
{
    public class StudentSummaryModel
    {
        public string NationalIdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TotalScore { get; set; }
        public string ApplicationStatus { get; set; }
    }
}
