using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_info
{
    public partial class quickAttendanceReport : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBranches();

                DateTime today = DateTime.Today;
                txtStartDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtEndDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtStartDateNepali.Text = NepaliDateConverter.ADToBSString(today);
                txtEndDateNepali.Text = NepaliDateConverter.ADToBSString(today);
            }
        }

        private void BindBranches()
        {
            ddlBranch.DataSource = blBranch.GetAllBranch();
            ddlBranch.DataTextField = "BranchName";
            ddlBranch.DataValueField = "BranchID";
            ddlBranch.DataBind();
            ddlBranch.Items.Insert(0, new ListItem("Select Branch", ""));
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment.Items.Clear();
            ddlEmployee.Items.Clear();
            ddlEmployee.Enabled = false;
            ClearEmployeeSelection();

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ddlDepartment.Enabled = false;
                return;
            }

            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
            ddlDepartment.Enabled = true;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmployee.Items.Clear();
            ClearEmployeeSelection();

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue) || string.IsNullOrWhiteSpace(ddlDepartment.SelectedValue))
            {
                ddlEmployee.Enabled = false;
                return;
            }

            int branchId = int.Parse(ddlBranch.SelectedValue);
            int departmentId = int.Parse(ddlDepartment.SelectedValue);

            ddlEmployee.DataSource = ble.GetEmployeeByBranchAndDepartment(branchId, departmentId);
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
            ddlEmployee.Enabled = true;
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlEmployee.SelectedValue))
            {
                ClearEmployeeSelection();
                return;
            }

            int employeeId = int.Parse(ddlEmployee.SelectedValue);
            hfEmployeeId.Value = employeeId.ToString();
            txtEmpId.Text = employeeId.ToString();
        }

        protected void txtEmpId_TextChanged(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtEmpId.Text.Trim(), out employeeId))
            {
                ClearEmployeeSelection();
                return;
            }

            DataTable dt = ble.GetEmployeeLookupById(employeeId);
            if (dt.Rows.Count == 0)
            {
                ClearEmployeeSelection();
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            hfEmployeeId.Value = employeeId.ToString();
        }

        private void ClearEmployeeSelection()
        {
            hfEmployeeId.Value = "";
            txtEmpId.Text = "";
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfEmployeeId.Value))
            {
                ShowAlert("Please select an Employee first.", "error");
                return;
            }

            DateTime startDate, endDate;
            if (!DateTime.TryParse(txtStartDateEnglish.Text.Trim(), out startDate) ||
                !DateTime.TryParse(txtEndDateEnglish.Text.Trim(), out endDate))
            {
                ShowAlert("Please provide valid Start and End dates.", "error");
                return;
            }

            if (endDate < startDate)
            {
                ShowAlert("End Date must be on or after Start Date.", "error");
                return;
            }

            int employeeId = int.Parse(hfEmployeeId.Value);

            DataTable empInfo = ble.GetEmployeeLookupById(employeeId);
            if (empInfo.Rows.Count == 0)
            {
                ShowAlert("Employee not found.", "error");
                return;
            }
            DataRow empRow = empInfo.Rows[0];

            lblStartDate.Text = NepaliDateConverter.ADToBSString(startDate);
            lblEndDate.Text = NepaliDateConverter.ADToBSString(endDate);
            lblEmployee.Text = empRow["EmployeeName"].ToString();
            lblEmployeeID.Text = employeeId.ToString();
            lblDesignation.Text = empRow["DesignationName"].ToString();
            lblDept.Text = empRow["DepartmentName"].ToString();
            lblBranch.Text = empRow["BranchName"].ToString();

            DataTable attendance = bla.GetAttendanceForReport(employeeId, startDate, endDate);

            DataTable report = BuildReportTable(attendance, startDate, endDate,
                out int totalDays, out int presentDays, out int absentDays,
                out int weekendDays, out double totalWorkedHours);

            gvReport.DataSource = report;
            gvReport.DataBind();

            lblTotalDays.Text = totalDays.ToString();
            lblPresentDays.Text = presentDays.ToString();
            lblAbsentDays.Text = absentDays.ToString();
            lblWeekend.Text = weekendDays.ToString();

            // Public Holiday / Leave require tables this project doesn't have
            // yet (a public holiday calendar and a leave application/approval
            // table). Left at 0 rather than guessing — see chat notes for
            // what each needs.
            lblPH.Text = "0";
            lblLeaveCount.Text = "0.00";
            lblWOW.Text = "0";
            lblWOPH.Text = "0";
            lblLWOP.Text = "0";

            lblWHRS.Text = totalWorkedHours.ToString("0.00");
            lblTotalPaybleDays.Text = (presentDays + weekendDays).ToString(); // + PH + Leave, once those exist

            pnlResults.Visible = true;
        }

        // Builds one row per calendar date in [startDate, endDate]. Saturdays
        // are flagged IsWeekend (and don't count toward Absent). Dates with a
        // matching tblAttendance record are Present; everything else is Absent.
        private DataTable BuildReportTable(DataTable attendance, DateTime startDate, DateTime endDate,
            out int totalDays, out int presentDays, out int absentDays, out int weekendDays,
            out double totalWorkedHours)
        {
            DataTable report = new DataTable();
            report.Columns.Add("DateNepali", typeof(string));
            report.Columns.Add("Day", typeof(string));
            report.Columns.Add("Roster", typeof(string));
            report.Columns.Add("InTime", typeof(string));
            report.Columns.Add("InDiff", typeof(string));
            report.Columns.Add("InMode", typeof(string));
            report.Columns.Add("OutTime", typeof(string));
            report.Columns.Add("OutDiff", typeof(string));
            report.Columns.Add("OutMode", typeof(string));
            report.Columns.Add("WorkedHour", typeof(string));
            report.Columns.Add("DayRemarks", typeof(string));
            report.Columns.Add("IsWeekend", typeof(bool)); // drives the merged "Weekend Holiday" row in RowDataBound

            var byDate = new System.Collections.Generic.Dictionary<DateTime, DataRow>();
            foreach (DataRow r in attendance.Rows)
            {
                byDate[((DateTime)r["AttendanceDateEnglish"]).Date] = r;
            }

            totalDays = 0;
            presentDays = 0;
            weekendDays = 0;
            totalWorkedHours = 0;

            for (DateTime date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                totalDays++;
                DataRow reportRow = report.NewRow();
                reportRow["DateNepali"] = NepaliDateConverter.ADToBSString(date);
                reportRow["Day"] = date.ToString("ddd");

                bool isWeekend = date.DayOfWeek == DayOfWeek.Saturday; // single weekly-off day per current schema
                reportRow["IsWeekend"] = isWeekend;

                if (isWeekend)
                {
                    // Weekend rows render as a single merged "Weekend Holiday"
                    // bar (see gvReport_RowDataBound), so the individual
                    // in/out fields are irrelevant here and left blank.
                    weekendDays++;
                    reportRow["Roster"] = "";
                    reportRow["InTime"] = "";
                    reportRow["InDiff"] = "";
                    reportRow["InMode"] = "";
                    reportRow["OutTime"] = "";
                    reportRow["OutDiff"] = "";
                    reportRow["OutMode"] = "";
                    reportRow["WorkedHour"] = "";
                    reportRow["DayRemarks"] = "Weekend";
                }
                else if (byDate.TryGetValue(date, out DataRow att))
                {
                    presentDays++;

                    string attendanceType = att["AttendanceType"].ToString();
                    string shiftName = att["ShiftName"] == DBNull.Value ? null : att["ShiftName"].ToString();
                    string startTime = att["StartTime"] == DBNull.Value ? null : att["StartTime"].ToString();
                    string endTime = att["EndTime"] == DBNull.Value ? null : att["EndTime"].ToString();
                    string createdBy = att["CreatedBy"] == DBNull.Value || att["CreatedBy"] == null
                        ? "System"
                        : att["CreatedBy"].ToString();

                    reportRow["Roster"] = string.IsNullOrEmpty(shiftName) ? "Not Assigned" : shiftName;

                    bool hasIn = attendanceType == "SignIn" || attendanceType == "Both";
                    bool hasOut = attendanceType == "SignOut" || attendanceType == "Both";

                    if (hasIn && !string.IsNullOrEmpty(startTime))
                    {
                        reportRow["InTime"] = startTime;
                        reportRow["InDiff"] = "-0Min";
                        reportRow["InMode"] = "Forced By " + createdBy;
                    }
                    else
                    {
                        reportRow["InTime"] = "";
                        reportRow["InDiff"] = "";
                        reportRow["InMode"] = "";
                    }

                    if (hasOut && !string.IsNullOrEmpty(endTime))
                    {
                        reportRow["OutTime"] = endTime;
                        reportRow["OutDiff"] = "0Min";
                        reportRow["OutMode"] = "Forced By " + createdBy;
                    }
                    else
                    {
                        reportRow["OutTime"] = "";
                        reportRow["OutDiff"] = "";
                        reportRow["OutMode"] = "";
                    }

                    double hoursWorked = 0;
                    if (hasIn && hasOut && !string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime)
                        && DateTime.TryParse(startTime, out DateTime startParsed)
                        && DateTime.TryParse(endTime, out DateTime endParsed))
                    {
                        TimeSpan worked = endParsed - startParsed;
                        if (worked.TotalMinutes < 0) worked = worked.Add(TimeSpan.FromHours(24));
                        hoursWorked = worked.TotalHours;
                        reportRow["WorkedHour"] = $"{(int)worked.TotalHours}hrs{worked.Minutes}min";
                    }
                    else
                    {
                        reportRow["WorkedHour"] = "";
                    }

                    totalWorkedHours += hoursWorked;
                    reportRow["DayRemarks"] = "Present";
                }
                else
                {
                    reportRow["Roster"] = "";
                    reportRow["InTime"] = "";
                    reportRow["InDiff"] = "";
                    reportRow["InMode"] = "";
                    reportRow["OutTime"] = "";
                    reportRow["OutDiff"] = "";
                    reportRow["OutMode"] = "";
                    reportRow["WorkedHour"] = "";
                    reportRow["DayRemarks"] = "Absent";
                }

                report.Rows.Add(reportRow);
            }

            absentDays = totalDays - presentDays - weekendDays;
            return report;
        }

        // Collapses Weekend and Absent rows into a single merged, centered,
        // colored cell spanning the whole row (matching the template's look).
        // Present rows are left alone and render their normal columns.
        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DataRowView drv = (DataRowView)e.Row.DataItem;
            bool isWeekend = (bool)drv["IsWeekend"];
            string dayRemarks = drv["DayRemarks"].ToString();

            string mergedText;
            string cssClass;

            if (isWeekend)
            {
                mergedText = "Weekend Holiday";
                cssClass = "weekend-holiday-row";
            }
            else if (dayRemarks == "Absent")
            {
                mergedText = "Absent";
                cssClass = "absent-row";
            }
            else
            {
                return; // Present rows: render normally, no merge
            }

            const int mergeStartIndex = 2; // first column after Date/Day
            int lastIndex = e.Row.Cells.Count - 1;
            int colSpan = lastIndex - mergeStartIndex + 1;

            TableCell mergedCell = e.Row.Cells[mergeStartIndex];
            mergedCell.ColumnSpan = colSpan;
            mergedCell.Text = mergedText;
            mergedCell.CssClass = cssClass;
            mergedCell.HorizontalAlign = HorizontalAlign.Center;

            for (int i = lastIndex; i > mergeStartIndex; i--)
            {
                e.Row.Cells.RemoveAt(i);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("quickAttendance.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            ShowAlert("Excel export isn't implemented yet.", "error");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"
                Swal.fire({{
                    title: '{message}',
                    icon: '{type}',
                    confirmButtonText: 'OK'
                }});
            ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}