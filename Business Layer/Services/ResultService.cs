using Business_Layer.Functions;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class ResultService : IResultService
    {
        public bool InputResult(ResultModel resultModel)
        {
            ResultRepository resultRepository = new ResultRepository();
            bool result = resultRepository.Insert(resultModel);
            ApplicationRepository applicationRepository = new ApplicationRepository();
            StudentRepository studentRepository = new StudentRepository();
            int currentStudentID = studentRepository.GetStudentID();
            CalculateScore newScore = new CalculateScore();
            ApplicationModel applicationModel;
            int totalScore;
            string applicationStatus;
            if (result)
            {
                totalScore = newScore.CalculateTotalScore(resultModel);
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
                result = applicationRepository.Insert(applicationModel);
            }
            return result;
        }
    }
}
