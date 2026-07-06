using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;

namespace TemplatingPractice.pages.attendance_management
{
    public partial class forceAttendance : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();

        private const string SessionKey = "PendingAttendanceRows";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session[SessionKey] = CreatePendingTable();
                BindGrid();
            }
        }

        private DataTable CreatePendingTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("EmployeeName", typeof(string));
            dt.Columns.Add("AttendanceDateEnglish", typeof(DateTime));
            dt.Columns.Add("AttendanceDateNepali", typeof(string));
            dt.Columns.Add("AttendanceType", typeof(string));
            return dt;
        }

        private DataTable PendingTable
        {
            get
            {
                if (Session[SessionKey] == null)
                    Session[SessionKey] = CreatePendingTable();
                return (DataTable)Session[SessionKey];
            }
        }

        private void BindGrid()
        {
            DataTable dt = PendingTable;
            gvAttendance.DataSource = dt;
            gvAttendance.DataBind();

            pnlAttendanceList.Visible = dt.Rows.Count > 0;
        }

        protected void txtId_TextChanged(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtId.Text.Trim(), out employeeId))
            {
                ClearEmployeeFields();
                return;
            }

            DataTable dt = ble.GetEmployeeLookupById(employeeId);
            if (dt.Rows.Count == 0)
            {
                ClearEmployeeFields();
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            DataRow row = dt.Rows[0];
            txtEmp.Text = row["EmployeeName"].ToString();
            txtDesignation.Text = row["DesignationName"].ToString();
            txtDept.Text = row["DepartmentName"].ToString();
            txtBranch.Text = row["BranchName"].ToString();
            hfEmployeeId.Value = employeeId.ToString();
        }

        private void ClearEmployeeFields()
        {
            txtEmp.Text = "";
            txtDesignation.Text = "";
            txtDept.Text = "";
            txtBranch.Text = "";
            hfEmployeeId.Value = "";
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(hfEmployeeId.Value))
            {
                ShowAlert("Please select a valid Employee ID first.", "error");
                return;
            }

            if (string.IsNullOrWhiteSpace(hfDateRangeJson.Value))
            {
                ShowAlert("Please select Start Date and Till Date.", "error");
                return;
            }

            int employeeId = int.Parse(hfEmployeeId.Value);
            string attendanceType = rbSignIn.Checked ? "SignIn" : rbSignOut.Checked ? "SignOut" : "Both";

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, string>> dateRange =
                serializer.Deserialize<List<Dictionary<string, string>>>(hfDateRangeJson.Value);

            DataTable dt = PendingTable;

            foreach (var entry in dateRange)
            {
                DateTime adDate = DateTime.ParseExact(entry["ad"], "yyyy-MM-dd", null);
                string bsDate = entry["bs"];

                // Skip duplicates already queued in this session
                bool alreadyQueued = false;
                foreach (DataRow r in dt.Rows)
                {
                    if ((int)r["EmployeeID"] == employeeId && (DateTime)r["AttendanceDateEnglish"] == adDate)
                    {
                        alreadyQueued = true;
                        break;
                    }
                }
                if (alreadyQueued) continue;

                DataRow newRow = dt.NewRow();
                newRow["EmployeeID"] = employeeId;
                newRow["EmployeeName"] = txtEmp.Text;
                newRow["AttendanceDateEnglish"] = adDate;
                newRow["AttendanceDateNepali"] = bsDate;
                newRow["AttendanceType"] = attendanceType;
                dt.Rows.Add(newRow);
            }

            Session[SessionKey] = dt;
            BindGrid();
        }

        protected void gvAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "removeRow")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                DataTable dt = PendingTable;
                dt.Rows[index].Delete();
                dt.AcceptChanges();
                Session[SessionKey] = dt;
                BindGrid();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = PendingTable;

            if (dt.Rows.Count == 0)
            {
                ShowAlert("Please add at least one attendance entry before saving.", "error");
                return;
            }

            int savedCount = 0;
            int skippedCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                int employeeId = (int)row["EmployeeID"];
                DateTime dateEnglish = (DateTime)row["AttendanceDateEnglish"];
                string dateNepali = row["AttendanceDateNepali"].ToString();
                string attendanceType = row["AttendanceType"].ToString();

                if (bla.AttendanceExists(employeeId, dateEnglish))
                {
                    skippedCount++;
                    continue;
                }

                bla.CreateAttendance(employeeId, dateEnglish, dateNepali, attendanceType);
                savedCount++;
            }

            Session[SessionKey] = CreatePendingTable();
            BindGrid();

            string message = savedCount + " attendance record(s) saved.";
            if (skippedCount > 0)
                message += " " + skippedCount + " skipped (already existed).";

            ShowAlert(message, "success");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session[SessionKey] = CreatePendingTable();
            Response.Redirect("forceAttendance.aspx");
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