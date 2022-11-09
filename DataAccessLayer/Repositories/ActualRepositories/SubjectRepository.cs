using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Common;
using System.Web.Security;
using System.Web;

namespace DataAccessLayer.Repositories.ActualRepositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDatabaseCommand _databaseCommand;
        private const string GetSubjectList = @"SELECT SubjectName FROM SubjectDetails WHERE IsDiscontinued=0";
        private const string GetCorrespondingSubjectIDBySubjectName = @"SELECT SubjectID 
                                                                        FROM SubjectDetails 
                                                                        WHERE SubjectName=@SubjectName";
        public SubjectRepository(IDatabaseCommand databaseCommand)
        {
            _databaseCommand = databaseCommand;
        }
        public List<string> GetListOfSubjects()
        {
            List<string> subjectNames = new List<string>();
            var dataTable = _databaseCommand.GetData(GetSubjectList);
            foreach (DataRow row in dataTable.Rows)
            {
                subjectNames.Add(row["SubjectName"].ToString());
            }
            return subjectNames;
        }

        public int GetSubjectIDBySubjectName(string subjectName)
        {
            int invalidSubjectID = -1;
            int subjectID = invalidSubjectID;
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@SubjectName", subjectName));
            var dataTable = _databaseCommand.GetDataWithConditions(GetCorrespondingSubjectIDBySubjectName, parameters);
            foreach (DataRow row in dataTable.Rows)
                subjectID = Convert.ToInt32(row["SubjectID"]);
            return subjectID;
        }
    }
}
