using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class AdminService : IAdminService
    {
        private readonly IStudentSummaryRepository _studentSummaryRepository;
        public AdminService(IStudentSummaryRepository studentSummaryRepository)
        {
            _studentSummaryRepository = studentSummaryRepository;
        }
        public List<StudentSummaryModel> GetSummaryOfStudents()
        {
            return _studentSummaryRepository.GetStudentSummary();
        }
    }
}
