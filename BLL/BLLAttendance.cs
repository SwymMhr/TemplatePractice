using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLAttendance
    {
        public bool AttendanceExists(int employeeId, DateTime dateEnglish)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@AttendanceDateEnglish", dateEnglish)
            };
            DataTable dt = DAO.GetTableQuery(
                "SELECT AttendanceID FROM tblAttendance WHERE EmployeeID = @EmployeeID AND AttendanceDateEnglish = @AttendanceDateEnglish",
                param);
            return dt.Rows.Count > 0;
        }

        public int CreateAttendance(int employeeId, DateTime dateEnglish, string dateNepali, string attendanceType)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@AttendanceDateEnglish", dateEnglish),
                new SqlParameter("@AttendanceDateNepali", (object)dateNepali ?? DBNull.Value),
                new SqlParameter("@AttendanceType", attendanceType)
            };

            string query = @"
                INSERT INTO tblAttendance (EmployeeID, AttendanceDateEnglish, AttendanceDateNepali, AttendanceType)
                VALUES (@EmployeeID, @AttendanceDateEnglish, @AttendanceDateNepali, @AttendanceType)";

            return DAO.ExecuteQuery(query, param);
        }

        public DataTable GetEmployeeLookupById(int employeeId)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };

            string query = @"
                            SELECT e.EmployeeID,
                                   e.EmployeeName,
                                   d.DesignationName,
                                   dep.DepartmentName,
                                   b.BranchName
                            FROM tblEmployee e
                            LEFT JOIN tblDesignation d ON e.DesignationID = d.DesignationID
                            LEFT JOIN tblDepartment dep ON e.DepartmentID = dep.DepartmentID
                            LEFT JOIN tblBranch b ON e.BranchID = b.BranchID
                            WHERE e.EmployeeID = @EmployeeID";

            return DAO.GetTableQuery(query, param);
        }
    }
}