using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLGrade
    {
        public DataTable GetAllGrade()
        {
            return DAO.GetTableQuery("SELECT * FROM tblGrade ORDER BY GradeID ASC", null);
        }

        public DataTable GetGradeById(int Gradeid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@GradeID",Gradeid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblGrade WHERE GradeID=@GradeID ORDER BY GradeID ASC ", param);
        }

        public int CreateGrade(string Gradename, string GradeType, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@GradeName",Gradename),
                new SqlParameter("@GradeType",GradeType),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("INSERT INTO tblGrade(GradeName,GradeType,Status) values (@GradeName,@GradeType,@Status)", param);
        }

        public int UpdateGrade(int Gradeid, string Gradename, string GradeType, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@GradeID",Gradeid),
                new SqlParameter("@GradeName",Gradename),
                new SqlParameter("@GradeType",GradeType),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("UPDATE tblGrade SET GradeName=@GradeName, GradeType=@GradeType, Status=@Status WHERE GradeID=@GradeID", param);
        }
    }
}