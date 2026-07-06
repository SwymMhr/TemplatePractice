using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLDesignation
    {
        public DataTable GetAllDesignation()
        {
            return DAO.GetTableQuery("SELECT * FROM tblDesignation ORDER BY DesignationID ASC", null);
        }

        public DataTable GetDesignationById(int Designationid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@DesignationID",Designationid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblDesignation WHERE DesignationID=@DesignationID ORDER BY DesignationID ASC ", param);
        }

        public int CreateDesignation(string Designationname, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@DesignationName",Designationname),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("INSERT INTO tblDesignation(DesignationName,Status) values (@DesignationName,@Status)", param);
        }

        public int UpdateDesignation(int Designationid, string Designationname, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@DesignationID",Designationid),
                new SqlParameter("@DesignationName",Designationname),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("UPDATE tblDesignation SET DesignationName=@DesignationName, Status=@Status WHERE DesignationID=@DesignationID", param);
        }
    }
}