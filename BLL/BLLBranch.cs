using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLBranch
    {
        public DataTable GetAllBranch()
        {
            return DAO.GetTableQuery("SELECT * FROM tblBranch ORDER BY BranchID ASC", null);
        }

        public DataTable GetBranchById(int branchid)
        {
            SqlParameter[] param = 
            {
                new SqlParameter("@BranchID",branchid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblBranch WHERE BranchID=@BranchID ORDER BY BranchID ASC ", param);
        }

        public int CreateBranch(string branchcode, string branchname, bool isoutbranch, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@BranchCode",branchcode),
                new SqlParameter("@BranchName",branchname),
                new SqlParameter("@IsOutBranch",isoutbranch),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("INSERT INTO tblBranch(BranchCode,BranchName,IsOutBranch,Status) values (@BranchCode,@BranchName,@IsOutBranch,@Status)", param);
        }

        public int UpdateBranch(int branchid, string branchcode, string branchname, bool isoutbranch, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@BranchID",branchid),
                new SqlParameter("@BranchCode",branchcode),
                new SqlParameter("@BranchName",branchname),
                new SqlParameter("@IsOutBranch",isoutbranch),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("UPDATE tblBranch SET BranchCode=@BranchCode, BranchName=@BranchName, IsOutBranch=@IsOutBranch, Status=@Status WHERE BranchID=@BranchID", param);
        }
    }
}