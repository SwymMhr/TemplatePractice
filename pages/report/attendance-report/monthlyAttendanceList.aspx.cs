using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class monthlyAttendanceList : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            int employeeId;
            if (!int.TryParse(Request.QueryString["UserID"], out employeeId))
            {
                Response.Redirect("~/monthlyAttendance");
                return;
            }

            DateTime startDate, endDate;
            if (!DateTime.TryParse(Request.QueryString["StartDate"], out startDate) ||
                !DateTime.TryParse(Request.QueryString["EndDate"], out endDate))
            {
                Response.Redirect("~/monthlyAttendance");
                return;
            }

            DataTable empInfo = ble.GetEmployeeLookupById(employeeId);
            if (empInfo.Rows.Count == 0)
            {
                Response.Redirect("~/monthlyAttendance");
                return;
            }
            DataRow empRow = empInfo.Rows[0];

            lblStartDate.Text = startDate.ToString("yyyy-MM-dd");
            lblEndDate.Text = endDate.ToString("yyyy-MM-dd");
            lblBranch.Text = empRow["BranchName"] == DBNull.Value ? "" : empRow["BranchName"].ToString();
            lblDept.Text = empRow["DepartmentName"] == DBNull.Value ? "" : empRow["DepartmentName"].ToString();

            DataTable attendance = bla.GetAttendanceForReport(employeeId, startDate, endDate);

            DataTable report = BuildSummaryTable(employeeId, empRow["EmployeeName"].ToString(), attendance, startDate, endDate);

            gvReport.DataSource = report;
            gvReport.DataBind();
        }

        private DataTable BuildSummaryTable(int employeeId, string employeeName, DataTable attendance,
            DateTime startDate, DateTime endDate)
        {
            var byDate = new Dictionary<DateTime, DataRow>();
            foreach (DataRow r in attendance.Rows)
            {
                byDate[((DateTime)r["AttendanceDateEnglish"]).Date] = r;
            }

            int totalDays = 0, weekendDays = 0, presentDays = 0, workedOnWeekend = 0;
            double totalWorkedHours = 0;

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                totalDays++;
                bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday;
                bool hasAttendance = byDate.TryGetValue(date, out DataRow att);

                double hoursWorked = 0;
                if (hasAttendance)
                {
                    string attendanceType = att["AttendanceType"].ToString();
                    string startTime = att["StartTime"] == DBNull.Value ? null : att["StartTime"].ToString();
                    string endTime = att["EndTime"] == DBNull.Value ? null : att["EndTime"].ToString();

                    bool hasIn = attendanceType == "SignIn" || attendanceType == "Both";
                    bool hasOut = attendanceType == "SignOut" || attendanceType == "Both";

                    if (hasIn && hasOut && !string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime)
                        && DateTime.TryParse(startTime, out DateTime startParsed)
                        && DateTime.TryParse(endTime, out DateTime endParsed))
                    {
                        TimeSpan worked = endParsed - startParsed;
                        if (worked.TotalMinutes < 0) worked = worked.Add(TimeSpan.FromHours(24));
                        hoursWorked = worked.TotalHours;
                    }
                }

                if (isWeekend)
                {
                    weekendDays++;
                    if (hasAttendance)
                    {
                        workedOnWeekend++;
                        totalWorkedHours += hoursWorked;
                    }
                }
                else if (hasAttendance)
                {
                    presentDays++;
                    totalWorkedHours += hoursWorked;
                }
            }

            const int phDays = 0;           
            const int totalLeaveDays = 0;   
            const int workedOnPH = 0;      

            int workingDays = totalDays - weekendDays - phDays;
            int absentDays = workingDays - presentDays - totalLeaveDays;
            int totalPresentDays = presentDays + workedOnWeekend + workedOnPH;

            DataTable report = new DataTable();
            report.Columns.Add("EmployeeDisplay", typeof(string));
            report.Columns.Add("TotalDays", typeof(int));
            report.Columns.Add("Weekend", typeof(int));
            report.Columns.Add("PH", typeof(int));
            report.Columns.Add("WorkingDay", typeof(int));
            report.Columns.Add("AbsentDays", typeof(int));
            report.Columns.Add("Kriya", typeof(string));
            report.Columns.Add("Maternity", typeof(string));
            report.Columns.Add("Paternity", typeof(string));
            report.Columns.Add("Isolation", typeof(string));
            report.Columns.Add("Travel", typeof(int));
            report.Columns.Add("PresentDays", typeof(int));
            report.Columns.Add("WorkedOnWeekend", typeof(int));
            report.Columns.Add("WorkedOnPH", typeof(int));
            report.Columns.Add("TotalPresentDays", typeof(int));
            report.Columns.Add("WorkedHours", typeof(string));

            DataRow row = report.NewRow();
            row["EmployeeDisplay"] = employeeName + " (" + employeeId + ")";
            row["TotalDays"] = totalDays;
            row["Weekend"] = weekendDays;
            row["PH"] = phDays;
            row["WorkingDay"] = workingDays;
            row["AbsentDays"] = absentDays;
            row["Kriya"] = "0.00";
            row["Maternity"] = "0.00";
            row["Paternity"] = "0.00";
            row["Isolation"] = "0.00";
            row["Travel"] = 0;
            row["PresentDays"] = presentDays;
            row["WorkedOnWeekend"] = workedOnWeekend;
            row["WorkedOnPH"] = workedOnPH;
            row["TotalPresentDays"] = totalPresentDays;
            row["WorkedHours"] = totalWorkedHours.ToString("0.00");
            report.Rows.Add(row);

            return report;
        }

        // Injects the "Leave Details" group header row above the normal GridView header row.
        // colspans: 6 (Employee..AbsentDays) + 4 (Kriya..Isolation) + 6 (Travel..WorkedHours) = 16
        protected void gvReport_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header) return;

            GridViewRow groupHeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell leftBlank = new TableCell { ColumnSpan = 6 };
            leftBlank.CssClass = "group-header-cell";

            TableCell leaveDetails = new TableCell { ColumnSpan = 4, Text = "Leave Details" };
            leaveDetails.CssClass = "group-header-cell";

            TableCell rightBlank = new TableCell { ColumnSpan = 6 };
            rightBlank.CssClass = "group-header-cell";

            groupHeaderRow.Cells.Add(leftBlank);
            groupHeaderRow.Cells.Add(leaveDetails);
            groupHeaderRow.Cells.Add(rightBlank);

            gvReport.Controls[0].Controls.AddAt(0, groupHeaderRow);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/monthlyAttendance");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ShowAlert("Excel export isn't implemented yet.", "error");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}