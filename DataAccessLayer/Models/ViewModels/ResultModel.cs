using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.ViewModels
{
    public class ResultModel
    {
        public List<string> SubjectNames { get; set; }
        public List<char> Grades { get; set; }
    }
}
