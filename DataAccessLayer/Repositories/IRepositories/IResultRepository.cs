using DataAccessLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.IRepositories
{
    public interface IResultRepository
    {
        bool InsertNewSetOfResults(ResultModel resultModel);
        bool ResultAlreadyBeenInput(int studentID);
    }
}
