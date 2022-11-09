using Business_Layer.Functions;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly IValidateResults _validateResults;
        public ResultService(IResultRepository resultRepository, IApplicationRepository applicationRepository, IStudentRepository studentRepository, ICalculateScore newScore, IValidateResults validateResults)
        {
            _resultRepository = resultRepository;
            _applicationRepository = applicationRepository;
            _studentRepository = studentRepository;
            _calculateScore = newScore;
            _validateResults = validateResults;
        }
        public List<ValidationResult> ValidatingResults(ResultModel resultModel)
        {
            return _validateResults.ResultsValidation(resultModel);
        }
        public bool InsertedResultsAndApplicationPlusUpdatedListOfApplicationStatus(ResultModel resultModel)
        {
            bool result = _resultRepository.InsertNewSetOfResults(resultModel);
            int currentStudentID = _studentRepository.GetStudentIDOfCurrentSession();
            ApplicationModel applicationModel;
            int totalScore;
            string applicationStatus;
            if (!result)
                return result;
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
            result = _applicationRepository.InsertNewApplication(applicationModel);
            if (!result)
                return result;
            result = _applicationRepository.UpdateListOfApplicationStatus();
            return result;
        }
    }
}
