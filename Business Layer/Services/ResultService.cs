using Business_Layer.Functions;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICalculateScore _calculateScore;
        public ResultService(IResultRepository resultRepository, IApplicationRepository applicationRepository, IStudentRepository studentRepository, ICalculateScore newScore)
        {
            _resultRepository = resultRepository;
            _applicationRepository = applicationRepository;
            _studentRepository = studentRepository;
            _calculateScore = newScore;
        }
        public bool InputResult(ResultModel resultModel)
        {
            //ResultRepository resultRepository = new ResultRepository();
            bool result = _resultRepository.Insert(resultModel);
            //ApplicationRepository applicationRepository = new ApplicationRepository();
            //StudentRepository studentRepository = new StudentRepository();
            int currentStudentID = _studentRepository.GetStudentID();
            //CalculateScore newScore = new CalculateScore();
            ApplicationModel applicationModel;
            int totalScore;
            string applicationStatus;
            if (result)
            {
                totalScore = _calculateScore.CalculateTotalScore(resultModel);
                if (totalScore < 10)
                    applicationStatus = "Rejected";
                else
                    applicationStatus = "Pending";
                applicationModel = new ApplicationModel()
                {
                    ApplicationStatus = applicationStatus,
                    StudentResultScore = totalScore,
                    StudentID = currentStudentID
                };
                result = _applicationRepository.Insert(applicationModel);
            }
            return result;
        }
    }
}
