﻿using DataAccessLayer.Entities;
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
        DAL _dal = new DAL();
        public List<string> getSubjectList()
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            List<string> subjectNames = new List<string>();
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT SubjectName FROM SubjectDetails WHERE IsDiscontinued=0", conn);
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subjectNames.Add(reader["SubjectName"].ToString());
                    }
                }
            }
            _dal.CloseConnection();
            return subjectNames;
        }

        public int GetSubjectID(string subjectName)
        {
            _dal.OpenConnection();
            SqlConnection conn = _dal.connection;
            List<string> subjectNames = new List<string>();
            int subjectID = 1;
            using (conn)
            {
                SqlCommand command = new SqlCommand("SELECT SubjectID FROM SubjectDetails WHERE SubjectName=@SubjectName", conn);
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue("@SubjectName", subjectName);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subjectID = (int)reader["SubjectID"];
                    }
                }
            }
            _dal.CloseConnection();
            return subjectID;
        }
    }
}