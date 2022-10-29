using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class StudentResult
    {
        public int StudentResultID { get; set; }
        public int SubjectID { get; set; } //Foreign Key
        public int StudentID { get; set; } //Foreign Key
        public char Grade { get; set; } //Foreign key
    }
}
