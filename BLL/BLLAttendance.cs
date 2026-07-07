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

        // shiftId is nullable — pass null when the employee has no shift
        // assigned for that weekday in tblEmployeeShift.
        public int CreateAttendance(int employeeId, DateTime attendanceDateEnglish, string attendanceDateNepali,
            string attendanceType, int? shiftId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@AttendanceDateEnglish", attendanceDateEnglish),
                new SqlParameter("@AttendanceDateNepali", (object)attendanceDateNepali ?? DBNull.Value),
                new SqlParameter("@AttendanceType", attendanceType),
                new SqlParameter("@ShiftID", (object)shiftId ?? DBNull.Value)
            };

            string query = @"INSERT INTO tblAttendance
                (EmployeeID, AttendanceDateEnglish, AttendanceDateNepali, AttendanceType, ShiftID)
                VALUES
                (@EmployeeID, @AttendanceDateEnglish, @AttendanceDateNepali, @AttendanceType, @ShiftID)";

            return DAO.ExecuteQuery(query, param);
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