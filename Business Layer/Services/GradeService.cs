using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }
        public List<GradeDetails> GetGradeList()
        {
            return _gradeRepository.GetListOfGradeDetails();
        }
    }
}
