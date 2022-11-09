using DataAccessLayer.Models.ViewModels;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DataAccessLayer.Common;
using DataAccessLayer.Entities;
using System.Net.Mail;
using System.Security.Principal;
using System.Collections;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly IDAL _dal;
        private readonly IDatabaseCommand _databaseCommand;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private const string GetFirstRowByStudentID = @"SELECT TOP 1 StudentID FROM StudentResult WHERE StudentID=@StudentID";
        private const string InsertIntoResult = @"INSERT INTO StudentResult(SubjectID, Grade, StudentID) VALUES(@SubjectID, @Grade, @StudentID)";
        public ResultRepository(IDAL dal, IStudentRepository studentRepository, ISubjectRepository subjectRepository, IDatabaseCommand databaseCommand)
        {
            _dal = dal;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _databaseCommand = databaseCommand;
        }
        public bool InsertNewSetOfResults(ResultModel resultModel)
        {
            _dal.OpenConnection();
            SqlTransaction transaction = _dal.Connection.BeginTransaction();
            try
            {
                int studentID = _studentRepository.GetStudentIDOfCurrentSession();
                using (SqlCommand command = new SqlCommand(InsertIntoResult, _dal.Connection, transaction))
                {
                    command.CommandType = CommandType.Text;
                    for (int i = 0; i < resultModel.SubjectNames.Count; i++)
                    {
                        command.Parameters.AddWithValue("@SubjectID", _subjectRepository.GetSubjectIDBySubjectName(resultModel.SubjectNames[i]));
                        command.Parameters.AddWithValue("@Grade", resultModel.Grades[i]);
                        command.Parameters.AddWithValue("@StudentID", studentID);
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
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
        public bool ResultAlreadyBeenInput(int studentID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@StudentID", studentID));
            var dataTable = _databaseCommand.GetDataWithConditions(GetFirstRowByStudentID, parameters);
            return (dataTable.Rows.Count > 0);
        }
    }
}
