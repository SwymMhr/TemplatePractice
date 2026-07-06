using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLLeave
    {
        public DataTable GetAllLeave()
        {
            return DAO.GetTableQuery("SELECT * FROM tblLeave ORDER BY LeaveID ASC", null);
        }

        public DataTable GetLeaveById(int Leaveid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@LeaveID",Leaveid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblLeave WHERE LeaveID=@LeaveID ORDER BY LeaveID ASC ", param);
        }

        public int CreateLeave(string Leavename, string Leavetype, bool Cashable,bool MonthlyEarning, bool ExhaustAllLeaves , string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@LeaveName",Leavename),
                new SqlParameter("@Leavetype",Leavetype),
                new SqlParameter("@Cashable",Cashable),
                new SqlParameter("@MonthlyEarning",MonthlyEarning),
                new SqlParameter("@ExhaustAllLeaves",ExhaustAllLeaves),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("INSERT INTO tblLeave(LeaveName,Leavetype,Cashable,MonthlyEarning,ExhaustAllLeaves,Status) values (@LeaveName,@LeaveType,@Cashable,@MonthlyEarning,@ExhaustAllLeaves,@Status)", param);
        }

        public int UpdateLeave(int Leaveid, string Leavename, string Leavetype, bool Cashable, bool MonthlyEarning, bool ExhaustAllLeaves, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@LeaveID",Leaveid),
                new SqlParameter("@LeaveName",Leavename),
                new SqlParameter("@Leavetype",Leavetype),
                new SqlParameter("@Cashable",Cashable),
                new SqlParameter("@MonthlyEarning",MonthlyEarning),
                new SqlParameter("@ExhaustAllLeaves",ExhaustAllLeaves),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("UPDATE tblLeave SET LeaveName=@LeaveName, Leavetype=@Leavetype, Cashable=@Cashable, MonthlyEarning=@MonthlyEarning, ExhaustAllLeaves=@ExhaustAllLeaves, Status=@Status WHERE LeaveID=@LeaveID", param);
        }
    }
}