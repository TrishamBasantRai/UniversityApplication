using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface ISubjectRepository
    {
        List<string> getSubjectList();
        int GetSubjectID(string subjectName);
    }
}
