using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.pages.system_setup;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages
{
    public partial class logHistory : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterEmployeeData();

                DateTime today = DateTime.Today;
                txtStartDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtEndDateEnglish.Text = today.ToString("yyyy-MM-dd");
                txtStartDateNepali.Text = NepaliDateConverter.ADToBSString(today);
                txtEndDateNepali.Text = NepaliDateConverter.ADToBSString(today);
            }
        }

        private void RegisterEmployeeData()
        {
            DataTable dt = ble.GetAllEmployee();

            var list = new List<object>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new
                {
                    id = Convert.ToInt32(row["EmployeeID"]),
                    name = row["EmployeeName"].ToString()
                });
            }

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = serializer.Serialize(list);

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "employeeData", "var allEmployees = " + json + ";", true);
        }

        private void ClearEmployeeSelection()
        {
            hfEmployeeId.Value = "";
            txtEmployeeSearch.Text = "";
            txtEmpId.Text = "";
        }

        protected void txtEmpId_TextChanged(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtEmpId.Text.Trim(), out employeeId))
            {
                ClearEmployeeSelection();
                return;
            }

            DataTable empTable = ble.GetEmployeeById(employeeId);
            if (empTable.Rows.Count == 0)
            {
                ClearEmployeeSelection();
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            DataRow empRow = empTable.Rows[0];
            hfEmployeeId.Value = employeeId.ToString();
            txtEmployeeSearch.Text = empRow["EmployeeName"].ToString() + " (" + employeeId + ")";
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {

            if (!DateTime.TryParse(txtStartDateEnglish.Text.Trim(), out DateTime startDate) ||
                !DateTime.TryParse(txtEndDateEnglish.Text.Trim(), out DateTime endDate))
            {
                ShowAlert("Please provide valid Start and End dates.", "error");
                return;
            }

            if (endDate < startDate)
            {
                ShowAlert("End Date must be on or after Start Date.", "error");
                return;
            }

            string url = "~/logHistoryList"
                + "?UserID=" + hfEmployeeId.Value
                + "&StartDate=" + startDate.ToString("yyyy-MM-dd")
                + "&EndDate=" + endDate.ToString("yyyy-MM-dd");

            Response.Redirect(url);
        }

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
                return;
            }

            const int mergeStartIndex = 2;
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
            Response.Redirect("~/LogHistory");
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