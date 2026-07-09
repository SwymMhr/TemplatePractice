using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.attendance_management
{
    public partial class forceAttendance : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();
        BLLEmployeeShift bles = new BLLEmployeeShift();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtId_TextChanged(object sender, EventArgs e)
        {
            int employeeId = Convert.ToInt32(txtId.Text);

            DataTable dt = ble.GetEmployeeLookupById(employeeId);
            if (dt.Rows.Count == 0)
            {
                ClearEmployeeFields();
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            txtEmp.Text = dt.Rows[0]["EmployeeName"].ToString();
            txtDesignation.Text = dt.Rows[0]["DesignationName"].ToString();
            txtDept.Text = dt.Rows[0]["DepartmentName"].ToString();
            txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
            hfEmployeeID.Value = employeeId.ToString();
        }

        private void ClearEmployeeFields()
        {
            txtEmp.Text = "";
            txtDesignation.Text = "";
            txtDept.Text = "";
            txtBranch.Text = "";
            hfEmployeeID.Value = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfEmployeeID.Value))
            {
                ShowAlert("Please select a valid Employee ID first.", "error");
                return;
            }

            if (!DateTime.TryParse(txtStartDateEnglish.Text.Trim(), out DateTime startDate) ||
                !DateTime.TryParse(txtEndDateEnglish.Text.Trim(), out DateTime endDate))
            {
                ShowAlert("Please provide valid Start and Till dates.", "error");
                return;
            }

            if (endDate < startDate)
            {
                ShowAlert("Till Date must be on or after Start Date.", "error");
                return;
            }

            int employeeId = Convert.ToInt32(hfEmployeeID.Value);

            string userType = ble.GetEmployeeById(employeeId).Rows[0]["UserType"].ToString();

            DataTable shiftsTable = bles.GetEmployeeShifts(employeeId);
            Dictionary<string, DataRow> shiftsByWeekDay = new Dictionary<string, DataRow>();
            foreach (DataRow shiftRow in shiftsTable.Rows)
            {
                shiftsByWeekDay[shiftRow["WeekDay"].ToString()] = shiftRow;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("AttendanceDateEnglish", typeof(DateTime));
            dt.Columns.Add("AttendanceDateNepali", typeof(string));
            dt.Columns.Add("ShiftID", typeof(int));
            dt.Columns.Add("ShiftName", typeof(string));
            dt.Columns.Add("InTime", typeof(string));
            dt.Columns.Add("OutTime", typeof(string));
            dt.Columns.Add("UserType", typeof(string));

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                DataRow row = dt.NewRow();
                row["AttendanceDateEnglish"] = date;
                row["AttendanceDateNepali"] = NepaliDateConverter.ADToBSString(date);
                row["UserType"] = userType;

                string weekDay = date.DayOfWeek.ToString();
                if (shiftsByWeekDay.ContainsKey(weekDay))
                {
                    DataRow shift = shiftsByWeekDay[weekDay];
                    row["ShiftID"] = Convert.ToInt32(shift["WorkHourID"]);
                    row["ShiftName"] = shift["ShiftName"].ToString();
                    row["InTime"] = shift["StartTime"].ToString();
                    row["OutTime"] = shift["EndTime"].ToString();
                }
                else
                {
                    row["ShiftID"] = DBNull.Value;
                    row["ShiftName"] = "Not Assigned";
                    row["InTime"] = "";
                    row["OutTime"] = "";
                }

                dt.Rows.Add(row);
            }

            gvAttendance.DataSource = dt;
            gvAttendance.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfEmployeeID.Value))
            {
                ShowAlert("Please select a valid Employee ID first.", "error");
                return;
            }

            int employeeId = Convert.ToInt32(hfEmployeeID.Value);
            string attendanceType = rbSignIn.Checked ? "SignIn" : rbSignOut.Checked ? "SignOut" : "Both";

            // TODO: replace with however your session actually stores the logged-in
            // user's display name once the tblUser/tblEmployee auth question is settled.
            string createdBy = Session["Username"] != null ? Session["Username"].ToString() : "Admin";

            int savedCount = 0;
            int skippedCount = 0;

            foreach (GridViewRow row in gvAttendance.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect == null || !chkSelect.Checked) continue;

                DateTime attendanceDate = (DateTime)gvAttendance.DataKeys[row.RowIndex]["AttendanceDateEnglish"];
                object shiftIdValue = gvAttendance.DataKeys[row.RowIndex]["ShiftID"];
                int? shiftId = (shiftIdValue == null || shiftIdValue == DBNull.Value)
                    ? (int?)null
                    : Convert.ToInt32(shiftIdValue);

                if (bla.AttendanceExists(employeeId, attendanceDate))
                {
                    skippedCount++;
                    continue;
                }

                string nepaliDate = NepaliDateConverter.ADToBSString(attendanceDate);

                bla.CreateAttendance(employeeId, attendanceDate, nepaliDate, attendanceType, shiftId, createdBy);
                savedCount++;
            }

            if (savedCount == 0 && skippedCount == 0)
            {
                ShowAlert("Please select at least one date to save.", "error");
                return;
            }

            string message = savedCount + " attendance record(s) saved.";
            if (skippedCount > 0)
                message += " " + skippedCount + " skipped (already existed).";

            ShowAlert(message, "success");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("forceAttendance.aspx");
            return;
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}