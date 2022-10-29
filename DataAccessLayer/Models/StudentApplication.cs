using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class StudentApplication
    {
        public int StudentApplicationID { get; set; }
        public int StudentResultScore { get; set; }
        public string StudentApplicationStatus { get; set; }
        public int StudentID { get; set; } //Foreign key
    }
}
