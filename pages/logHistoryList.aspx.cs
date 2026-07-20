using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages
{
    public partial class logHistoryList : System.Web.UI.Page
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

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (gvLogHistory.Rows.Count > 0)
            {
                gvLogHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvLogHistory.UseAccessibleHeader = true;
            }
        }

        private void LoadReport()
        {
            if (!int.TryParse(Request.QueryString["UserID"], out int employeeId))
            {
                Response.Redirect("~/logHistory");
                return;
            }

            if (!DateTime.TryParse(Request.QueryString["StartDate"], out DateTime startDate) ||
                !DateTime.TryParse(Request.QueryString["EndDate"], out DateTime endDate))
            {
                Response.Redirect("~/logHistory");
                return;
            }

            DataTable empTable = ble.GetEmployeeById(employeeId);
            if (empTable.Rows.Count == 0)
            {
                Response.Redirect("~/logHistory");
                return;
            }

            hfEmployee.Value = empTable.Rows[0]["EmployeeName"].ToString() + " (" + employeeId + ")";
            hfStartDate.Value = NepaliDateConverter.ADToBSString(startDate);
            hfEndDate.Value = NepaliDateConverter.ADToBSString(endDate);

            DataTable source = bla.GetAttendanceLogSource(employeeId, startDate, endDate);
            DataTable logs = BuildLogRows(source);

            gvLogHistory.DataSource = logs;
            gvLogHistory.DataBind();
        }

        private DataTable BuildLogRows(DataTable source)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AttendanceID", typeof(int));
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("VerifyMode", typeof(string));
            dt.Columns.Add("Mode", typeof(string));
            dt.Columns.Add("LogDate", typeof(DateTime));
            dt.Columns.Add("LogTime", typeof(string));

            foreach (DataRow src in source.Rows)
            {
                int attendanceId = Convert.ToInt32(src["AttendanceID"]);
                int employeeId = Convert.ToInt32(src["EmployeeID"]);
                DateTime attDate = (DateTime)src["AttendanceDateEnglish"];
                string attendanceType = src["AttendanceType"].ToString();
                string startTime = src["StartTime"] == DBNull.Value ? null : src["StartTime"].ToString();
                string endTime = src["EndTime"] == DBNull.Value ? null : src["EndTime"].ToString();

                bool hasIn = attendanceType == "SignIn" || attendanceType == "Both";
                bool hasOut = attendanceType == "SignOut" || attendanceType == "Both";

                if (hasIn)
                {
                    DataRow row = dt.NewRow();
                    row["AttendanceID"] = attendanceId;
                    row["EmployeeID"] = employeeId;
                    row["VerifyMode"] = "Finger";
                    row["Mode"] = "In";
                    row["LogDate"] = attDate;
                    row["LogTime"] = string.IsNullOrEmpty(startTime) ? "" : startTime;
                    dt.Rows.Add(row);
                }

                if (hasOut)
                {
                    DataRow row = dt.NewRow();
                    row["AttendanceID"] = attendanceId;
                    row["EmployeeID"] = employeeId;
                    row["VerifyMode"] = "Finger";
                    row["Mode"] = "Out";
                    row["LogDate"] = attDate;
                    row["LogTime"] = string.IsNullOrEmpty(endTime) ? "" : endTime;
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        protected void gvLogHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateLog")
            {

            }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/logHistory");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }

    }
}