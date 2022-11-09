using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class RedirectService : IRedirectService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IResultRepository _resultRepository;
        public RedirectService(IStudentRepository studentRepository, IResultRepository resultRepository)
        {
            _studentRepository = studentRepository;
            _resultRepository = resultRepository;
        }
        public bool HasAlreadyInputDetails()
        {
            int studentID = _studentRepository.GetStudentIDOfCurrentSession();
            int studentIDDoesNotExist = -1;
            return (studentID != studentIDDoesNotExist);
        }
        public bool HasAlreadyInputResults()
        {
            int studentID = _studentRepository.GetStudentIDOfCurrentSession();
            return (_resultRepository.ResultAlreadyBeenInput(studentID));
        }
    }
}
