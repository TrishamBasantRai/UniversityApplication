using DataAccessLayer.Entities;
using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Common;
using System.Web;
using Unity.Policy;
using System.Security.Principal;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly IDAL _dal;
        private readonly IDatabaseCommand _databaseCommand;
        private const string InsertNewStudentApplication = @"INSERT INTO StudentApplication(StudentResultScore, StudentApplicationStatus, StudentID) 
                                                             VALUES(@StudentResultScore, @StudentApplicationStatus, @StudentID)";
        private const string GetStudentApplicationStatusDetails = @"SELECT FirstName, LastName, StudentResultScore, StudentApplicationStatus 
                                                                    FROM StudentDetails 
                                                                    INNER JOIN StudentApplication 
                                                                    ON StudentDetails.StudentID = StudentApplication.StudentID 
                                                                    WHERE StudentDetails.UserAccountID = @UserAccountID";
        private const string UpdateApprovedToPending = @"UPDATE StudentApplication SET StudentApplicationStatus='Pending' WHERE StudentApplicationStatus='Approved'";
        private const string UpdateTopFifteenPendingToApproved = @"UPDATE StudentApplication 
                                                                   SET StudentApplicationStatus = 'Approved' WHERE StudentID IN
                                                                        (SELECT TOP 15 StudentID 
                                                                         FROM StudentApplication 
                                                                         WHERE StudentApplicationStatus = 'Pending' 
                                                                         ORDER BY StudentResultScore)";
        public ApplicationRepository(IDAL dal, IDatabaseCommand databaseCommand)
        {
            _dal = dal;
            _databaseCommand = databaseCommand;
        }
        public bool InsertNewApplication(ApplicationModel applicationModel)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@StudentResultScore", applicationModel.StudentResultScore));
                parameters.Add(new SqlParameter("@StudentApplicationStatus", applicationModel.ApplicationStatus));
                parameters.Add(new SqlParameter("@StudentID", applicationModel.StudentID));
                return _databaseCommand.InsertUpdateData(InsertNewStudentApplication, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ApplicationStatusModel GetApplicationStatusDetailsOfStudent()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@UserAccountID", HttpContext.Current.Session["userAccountID"]));
            ApplicationStatusModel applicationStatus = new ApplicationStatusModel();
            var dataTable = _databaseCommand.GetDataWithConditions(GetStudentApplicationStatusDetails, parameters);
            foreach (DataRow row in dataTable.Rows)
            {
                applicationStatus.FirstName = row["FirstName"].ToString();
                applicationStatus.LastName = row["LastName"].ToString();
                applicationStatus.TotalPoints = Convert.ToInt32(row["StudentResultScore"]);
                applicationStatus.ApplicationStatus = row["StudentApplicationStatus"].ToString();
            }
            return applicationStatus;
        }
        public bool UpdateListOfApplicationStatus()
        {
            _dal.OpenConnection();
            SqlTransaction transaction = _dal.Connection.BeginTransaction();
            try
            {
                using (SqlCommand command = new SqlCommand(UpdateApprovedToPending, _dal.Connection, transaction))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(UpdateTopFifteenPendingToApproved, _dal.Connection, transaction))
                {
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
                _dal.CloseConnection();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
