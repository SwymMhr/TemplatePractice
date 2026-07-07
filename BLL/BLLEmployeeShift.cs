using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLEmployeeShift
    {
        public DataTable GetShiftsByEmployee(int employeeId)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };
            return DAO.GetTableQuery(@"
                SELECT es.EmployeeID, es.WeekDay, es.WorkHourID, w.ShiftName
                FROM tblEmployeeShift es
                JOIN tblWorkHour w ON es.WorkHourID = w.WorkHourID
                WHERE es.EmployeeID = @EmployeeID", param);
        }

        // Used by forceAttendance.aspx.cs to find which shift an employee
        // is on for a given weekday, so ShiftID can be populated on save.
        public DataRow GetShiftForEmployeeAndWeekday(int employeeId, string weekDay)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@WeekDay", weekDay)
            };
            DataTable dt = DAO.GetTableQuery(@"
                SELECT es.WorkHourID, w.ShiftName
                FROM tblEmployeeShift es
                JOIN tblWorkHour w ON es.WorkHourID = w.WorkHourID
                WHERE es.EmployeeID = @EmployeeID AND es.WeekDay = @WeekDay", param);
            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }

        public bool ShiftExists(int employeeId, string weekDay)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@WeekDay", weekDay)
            };
            DataTable dt = DAO.GetTableQuery(
                "SELECT 1 FROM tblEmployeeShift WHERE EmployeeID = @EmployeeID AND WeekDay = @WeekDay", param);
            return dt.Rows.Count > 0;
        }

        // Insert if this employee/weekday combo has no shift yet, otherwise
        // overwrite the existing one — this is the "just like Branch/Department" behavior.
        public int UpsertEmployeeShift(int employeeId, string weekDay, int workHourId)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@WeekDay", weekDay),
                new SqlParameter("@WorkHourID", workHourId)
            };

            if (ShiftExists(employeeId, weekDay))
            {
                return DAO.ExecuteQuery(
                    "UPDATE tblEmployeeShift SET WorkHourID = @WorkHourID WHERE EmployeeID = @EmployeeID AND WeekDay = @WeekDay",
                    param);
            }

            return DAO.ExecuteQuery(
                "INSERT INTO tblEmployeeShift (EmployeeID, WeekDay, WorkHourID) VALUES (@EmployeeID, @WeekDay, @WorkHourID)",
                param);
        }

        public int DeleteEmployeeShift(int employeeId, string weekDay)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@WeekDay", weekDay)
            };
            return DAO.ExecuteQuery(
                "DELETE FROM tblEmployeeShift WHERE EmployeeID = @EmployeeID AND WeekDay = @WeekDay", param);
        }

        public int DeleteAllShiftsForEmployee(int employeeId)
        {
            SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };
            return DAO.ExecuteQuery("DELETE FROM tblEmployeeShift WHERE EmployeeID = @EmployeeID", param);
        }
    }
}