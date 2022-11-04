using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.ActualRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Business_Layer.Functions
{
    public class CalculateScore : ICalculateScore
    {
        public int CalculateTotalScore(ResultModel resultModel)
        {
            GradeRepository gradeRepository = new GradeRepository();
            List<GradeDetails> gradeDetailsList = new List<GradeDetails>();
            gradeDetailsList = gradeRepository.GetGradeDetails();
            Dictionary<char, byte> gradeAssociation = new Dictionary<char, byte>();
            for (int i = 0; i < gradeDetailsList.Count; i++)
                gradeAssociation.Add(gradeDetailsList[i].Grade, gradeDetailsList[i].GradePoints);
            int totalScore = 0;
            for(int i=0; i < resultModel.Grades.Count; i++)
                totalScore = totalScore + gradeAssociation[resultModel.Grades[i]];
            return totalScore;
        }
    }
}
