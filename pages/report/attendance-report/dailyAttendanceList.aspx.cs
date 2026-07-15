using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class dailyAttendanceList : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
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
            int branchId;
            if (!int.TryParse(Request.QueryString["BranchID"], out branchId))
            {
                Response.Redirect("dailyAttendance.aspx");
                return;
            }

            int? departmentId = null;
            if (int.TryParse(Request.QueryString["DepartmentID"], out int deptParsed))
            {
                departmentId = deptParsed;
            }

            DateTime date;
            if (!DateTime.TryParse(Request.QueryString["StartDate"], out date))
            {
                Response.Redirect("dailyAttendance.aspx");
                return;
            }

            lblDate.Text = NepaliDateConverter.ADToBSString(date);

            DataTable branchTable = blBranch.GetBranchById(branchId);
            string branchName = branchTable.Rows.Count > 0 ? branchTable.Rows[0]["BranchName"].ToString() : "";

            DataTable employees = ble.GetEmployeesForAttendanceReport(branchId, departmentId);
            DataTable attendance = bla.GetAttendanceInRangeForEmployees(date, date, branchId, departmentId);

            var attendanceByEmployee = new Dictionary<int, DataRow>();
            foreach (DataRow row in attendance.Rows)
            {
                attendanceByEmployee[Convert.ToInt32(row["EmployeeID"])] = row;
            }

            DataTable report = BuildReportTable(employees, attendanceByEmployee, branchName);

            gvReport.DataSource = report;
            gvReport.DataBind();
        }

        private DataTable BuildReportTable(DataTable employees, Dictionary<int, DataRow> attendanceByEmployee, string branchName)
        {
            DataTable report = new DataTable();
            report.Columns.Add("EmployeeID", typeof(int));
            report.Columns.Add("EmployeeName", typeof(string));
            report.Columns.Add("BranchName", typeof(string));
            report.Columns.Add("DepartmentName", typeof(string));
            report.Columns.Add("InTime", typeof(string));
            report.Columns.Add("OutTime", typeof(string));
            report.Columns.Add("Remarks", typeof(string));
            report.Columns.Add("IsAbsent", typeof(bool));

            foreach (DataRow emp in employees.Rows)
            {
                int employeeId = Convert.ToInt32(emp["EmployeeID"]);
                DataRow reportRow = report.NewRow();
                reportRow["EmployeeID"] = employeeId;
                reportRow["EmployeeName"] = emp["EmployeeName"].ToString();
                reportRow["BranchName"] = branchName;
                reportRow["DepartmentName"] = emp["DepartmentName"] == DBNull.Value ? "" : emp["DepartmentName"].ToString();

                if (attendanceByEmployee.TryGetValue(employeeId, out DataRow att))
                {
                    string attendanceType = att["AttendanceType"].ToString();
                    string startTime = att["StartTime"] == DBNull.Value ? null : att["StartTime"].ToString();
                    string endTime = att["EndTime"] == DBNull.Value ? null : att["EndTime"].ToString();

                    bool hasIn = attendanceType == "SignIn" || attendanceType == "Both";
                    bool hasOut = attendanceType == "SignOut" || attendanceType == "Both";

                    reportRow["InTime"] = (hasIn && !string.IsNullOrEmpty(startTime)) ? startTime : "";
                    reportRow["OutTime"] = (hasOut && !string.IsNullOrEmpty(endTime)) ? endTime : "";
                    reportRow["Remarks"] = "Present";
                    reportRow["IsAbsent"] = false;
                }
                else
                {
                    reportRow["InTime"] = "";
                    reportRow["OutTime"] = "";
                    reportRow["Remarks"] = "Absent";
                    reportRow["IsAbsent"] = true;
                }

                report.Rows.Add(reportRow);
            }

            return report;
        }

        protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DataRowView drv = (DataRowView)e.Row.DataItem;
            bool isAbsent = (bool)drv["IsAbsent"];

            if (!isAbsent) return;

            const int mergeStartIndex = 4; // In Time column
            int lastIndex = e.Row.Cells.Count - 1; // Remarks column
            int colSpan = lastIndex - mergeStartIndex + 1;

            TableCell mergedCell = e.Row.Cells[mergeStartIndex];
            mergedCell.ColumnSpan = colSpan;
            mergedCell.Text = "Absent";
            mergedCell.CssClass = "absent-row";
            mergedCell.HorizontalAlign = HorizontalAlign.Center;

            for (int i = lastIndex; i > mergeStartIndex; i--)
            {
                e.Row.Cells.RemoveAt(i);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("dailyAttendance.aspx");
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