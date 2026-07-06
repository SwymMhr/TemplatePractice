using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLDepartment
    {
        public DataTable GetAllDepartment()
        {
            return DAO.GetTableQuery("SELECT * FROM tblDepartment ORDER BY DepartmentID ASC", null);
        }

        public DataTable GetDepartmentById(int Departmentid)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@DepartmentID",Departmentid)
            };
            return DAO.GetTableQuery("SELECT * FROM tblDepartment WHERE DepartmentID=@DepartmentID ORDER BY DepartmentID ASC ", param);
        }

        public int CreateDepartment(string Departmentname, string Departmentcode, string status)
        {
            SqlParameter[] param =
            {               
                new SqlParameter("@DepartmentName",Departmentname),
                new SqlParameter("@DepartmentCode",Departmentcode),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("INSERT INTO tblDepartment(DepartmentName,DepartmentCode,Status) values (@DepartmentName,@DepartmentCode,@Status)", param);
        }

        public int UpdateDepartment(int Departmentid, string Departmentname, string Departmentcode, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@DepartmentID",Departmentid),
                new SqlParameter("@DepartmentName",Departmentname),
                new SqlParameter("@DepartmentCode",Departmentcode),
                new SqlParameter("@Status",status),
            };
            return DAO.ExecuteQuery("UPDATE tblDepartment SET DepartmentName=@DepartmentName, DepartmentCode=@DepartmentCode, Status=@Status WHERE DepartmentID=@DepartmentID", param);
        }
    }
}