using System;
using System.Data;
using System.Data.SqlClient;
using TemplatingPractice.DAL;

namespace TemplatingPractice.BLL
{
    public class BLLWorkHour
    {
        public DataTable GetAllWorkHour()
        {
            return DAO.GetTableQuery("SELECT * FROM tblWorkHour ORDER BY WorkHourID ASC", null);
        }

        public DataRow GetWorkHourById(int workHourId)
        {
            SqlParameter[] param = { new SqlParameter("@WorkHourID", workHourId) };
            DataTable dt = DAO.GetTableQuery("SELECT * FROM tblWorkHour WHERE WorkHourID = @WorkHourID", param);
            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }

        public DataTable GetActiveWorkHour()
        {
            SqlParameter[] param = { new SqlParameter("@Status", "Active") };
            return DAO.GetTableQuery("SELECT * FROM tblWorkHour WHERE Status = @Status ORDER BY ShiftName ASC", param);
        }

        public int CreateWorkHour(string shiftName, string startTime, string lateInBy, string totalHour,
            int lunchTime, string endTime, string lateOutBy, string shift, bool defaultForAllWeekend, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@ShiftName", shiftName),
                new SqlParameter("@StartTime", startTime),
                new SqlParameter("@LateInBy", lateInBy),
                new SqlParameter("@TotalHour", totalHour),
                new SqlParameter("@LunchTime", lunchTime),
                new SqlParameter("@EndTime", endTime),
                new SqlParameter("@LateOutBy", lateOutBy),
                new SqlParameter("@Shift", shift),
                new SqlParameter("@DefaultForAllWeekend", defaultForAllWeekend),
                new SqlParameter("@Status", status)
            };

            string query = @"INSERT INTO tblWorkHour
                (ShiftName, StartTime, LateInBy, TotalHour, LunchTime, EndTime, LateOutBy, Shift, DefaultForAllWeekend, Status)
                VALUES
                (@ShiftName, @StartTime, @LateInBy, @TotalHour, @LunchTime, @EndTime, @LateOutBy, @Shift, @DefaultForAllWeekend, @Status)";

            return DAO.ExecuteQuery(query, param);
        }

        public int UpdateStatus(int workHourId, string status)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@WorkHourID", workHourId),
                new SqlParameter("@Status", status)
            };
            return DAO.ExecuteQuery("UPDATE tblWorkHour SET Status = @Status WHERE WorkHourID = @WorkHourID", param);
        }

        public int DeleteWorkHour(int workHourId)
        {
            SqlParameter[] param = { new SqlParameter("@WorkHourID", workHourId) };
            return DAO.ExecuteQuery("DELETE FROM tblWorkHour WHERE WorkHourID = @WorkHourID", param);
        }
    }
}