using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLAttendance
    {
        public DataTable GetAllAttendance()
        {
            return DAO.GetTableQuery(@"
                SELECT a.AttendanceID, a.EmployeeID, e.EmployeeName, a.AttendanceDateEnglish, 
                       a.AttendanceDateNepali, a.AttendanceType, a.ShiftID, w.ShiftName, a.CreatedDate
                FROM tblAttendance a
                JOIN tblEmployee e ON a.EmployeeID = e.EmployeeID
                LEFT JOIN tblWorkHour w ON a.ShiftID = w.WorkHourID
                ORDER BY a.AttendanceDateEnglish DESC", null);
        }

        public DataTable GetAttendanceInRangeForEmployees(DateTime startDate, DateTime endDate, int branchId, int? departmentId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate),
                new SqlParameter("@BranchID", branchId),
                new SqlParameter("@DepartmentID", (object)departmentId ?? DBNull.Value)
            };

            string query = @"SELECT a.EmployeeID, a.AttendanceDateEnglish, a.AttendanceType, w.StartTime, w.EndTime
                      FROM tblAttendance a
                      JOIN tblEmployee e ON e.EmployeeID = a.EmployeeID
                      LEFT JOIN tblWorkHour w ON a.ShiftID = w.WorkHourID
                      WHERE a.AttendanceDateEnglish BETWEEN @StartDate AND @EndDate
                        AND e.BranchID = @BranchID
                        AND (@DepartmentID IS NULL OR e.DepartmentID = @DepartmentID)";

            return DAO.GetTableQuery(query, param);
        }

        public DataTable GetAttendanceLogSource(int employeeId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            string query = @"
                SELECT a.AttendanceID, a.EmployeeID, a.AttendanceDateEnglish, a.AttendanceType,
                       COALESCE(w1.StartTime, w2.StartTime) AS StartTime,
                       COALESCE(w1.EndTime, w2.EndTime) AS EndTime
                FROM tblAttendance a
                LEFT JOIN tblWorkHour w1 ON a.ShiftID = w1.WorkHourID
                LEFT JOIN tblEmployeeShift es ON es.EmployeeID = a.EmployeeID
                                               AND es.WeekDay = DATENAME(WEEKDAY, a.AttendanceDateEnglish)
                LEFT JOIN tblWorkHour w2 ON es.WorkHourID = w2.WorkHourID
                WHERE a.EmployeeID = @EmployeeID
                  AND a.AttendanceDateEnglish BETWEEN @StartDate AND @EndDate
                ORDER BY a.AttendanceDateEnglish ASC";

            return DAO.GetTableQuery(query, param);
        }

        public DataTable GetAttendanceByEmployee(int employeeId)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };
            return DAO.GetTableQuery(@"
                SELECT a.AttendanceID, a.AttendanceDateEnglish, a.AttendanceDateNepali, 
                       a.AttendanceType, a.ShiftID, w.ShiftName, a.CreatedDate
                FROM tblAttendance a
                LEFT JOIN tblWorkHour w ON a.ShiftID = w.WorkHourID
                WHERE a.EmployeeID = @EmployeeID
                ORDER BY a.AttendanceDateEnglish DESC", param);
        }

        public bool AttendanceExists(int employeeId, DateTime attendanceDateEnglish)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@AttendanceDateEnglish", attendanceDateEnglish)
            };
            DataTable dt = DAO.GetTableQuery(
                "SELECT AttendanceID FROM tblAttendance WHERE EmployeeID = @EmployeeID AND AttendanceDateEnglish = @AttendanceDateEnglish",
                param);
            return dt.Rows.Count > 0;
        }


        public int CreateAttendance(int employeeId, DateTime attendanceDateEnglish, string attendanceDateNepali,
            string attendanceType, int? shiftId, string createdBy)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@AttendanceDateEnglish", attendanceDateEnglish),
                new SqlParameter("@AttendanceDateNepali", (object)attendanceDateNepali ?? DBNull.Value),
                new SqlParameter("@AttendanceType", attendanceType),
                new SqlParameter("@ShiftID", (object)shiftId ?? DBNull.Value),
                new SqlParameter("@CreatedBy", (object)createdBy ?? DBNull.Value)
            };

            string query = @"INSERT INTO tblAttendance
                (EmployeeID, AttendanceDateEnglish, AttendanceDateNepali, AttendanceType, ShiftID, CreatedBy)
                VALUES
                (@EmployeeID, @AttendanceDateEnglish, @AttendanceDateNepali, @AttendanceType, @ShiftID, @CreatedBy)";

            return DAO.ExecuteQuery(query, param);
        }

        // Used by quickAttendanceReport.aspx.cs. Returns one row per attendance
        // record actually saved for this employee within the date range — days
        // with no matching row are treated as Absent by the report page.
        public DataTable GetAttendanceForReport(int employeeId, DateTime startDate, DateTime endDate)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };

            string query = @"
                SELECT a.AttendanceDateEnglish, a.AttendanceDateNepali, a.AttendanceType,
                       w.ShiftName, w.StartTime, w.EndTime, a.CreatedBy
                FROM tblAttendance a
                LEFT JOIN tblWorkHour w ON a.ShiftID = w.WorkHourID
                WHERE a.EmployeeID = @EmployeeID
                  AND a.AttendanceDateEnglish BETWEEN @StartDate AND @EndDate
                ORDER BY a.AttendanceDateEnglish ASC";

            return DAO.GetTableQuery(query, param);
        }

        public int UpdateAttendance(int attendanceId, string attendanceType, int? shiftId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@AttendanceID", attendanceId),
                new SqlParameter("@AttendanceType", attendanceType),
                new SqlParameter("@ShiftID", (object)shiftId ?? DBNull.Value)
            };
            return DAO.ExecuteQuery(
                "UPDATE tblAttendance SET AttendanceType = @AttendanceType, ShiftID = @ShiftID WHERE AttendanceID = @AttendanceID",
                param);
        }

        public int DeleteAttendance(int attendanceId)
        {
            SqlParameter[] param = { new SqlParameter("@AttendanceID", attendanceId) };
            return DAO.ExecuteQuery("DELETE FROM tblAttendance WHERE AttendanceID = @AttendanceID", param);
        }
    }
}