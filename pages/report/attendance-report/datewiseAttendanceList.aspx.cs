using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class datewiseAttendanceList : System.Web.UI.Page
    {
        BLLEmployee ble = new BLLEmployee();
        BLLAttendance bla = new BLLAttendance();

        private DateTime lastGroupDate = DateTime.MinValue;
        private int insertedGroupRows = 0; // tracks how many separator rows we've already inserted

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            if (!int.TryParse(Request.QueryString["BranchID"], out int branchId))
            {
                Response.Redirect("datewiseAttendance.aspx");
                return;
            }

            int? departmentId = null;
            if (int.TryParse(Request.QueryString["DepartmentID"], out int deptId))
                departmentId = deptId;

            if (!DateTime.TryParse(Request.QueryString["StartDate"], out DateTime startDate) ||
                !DateTime.TryParse(Request.QueryString["EndDate"], out DateTime endDate))
            {
                Response.Redirect("datewiseAttendance.aspx");
                return;
            }

            lblStartDate.Text = NepaliDateConverter.ADToBSString(startDate);
            lblEndDate.Text = NepaliDateConverter.ADToBSString(endDate);

            DataTable employees = ble.GetEmployeesForAttendanceReport(branchId, departmentId);
            DataTable attendance = bla.GetAttendanceInRangeForEmployees(startDate, endDate, branchId, departmentId);

            Dictionary<string, DataRow> attendanceLookup = new Dictionary<string, DataRow>();
            foreach (DataRow row in attendance.Rows)
            {
                string key = row["EmployeeID"] + "_" + Convert.ToDateTime(row["AttendanceDateEnglish"]).ToString("yyyy-MM-dd");
                attendanceLookup[key] = row;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("AttendanceDateEnglish", typeof(DateTime));
            dt.Columns.Add("AttendanceDateNepali", typeof(string));
            dt.Columns.Add("EmployeeID", typeof(int));
            dt.Columns.Add("EmployeeName", typeof(string));
            dt.Columns.Add("DepartmentName", typeof(string));
            dt.Columns.Add("InTime", typeof(string));
            dt.Columns.Add("OutTime", typeof(string));
            dt.Columns.Add("Remarks", typeof(string));
            dt.Columns.Add("IsAbsent", typeof(bool));

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                foreach (DataRow emp in employees.Rows)
                {
                    int employeeId = Convert.ToInt32(emp["EmployeeID"]);
                    string key = employeeId + "_" + date.ToString("yyyy-MM-dd");

                    DataRow row = dt.NewRow();
                    row["AttendanceDateEnglish"] = date;
                    row["AttendanceDateNepali"] = NepaliDateConverter.ADToBSString(date);
                    row["EmployeeID"] = employeeId;
                    row["EmployeeName"] = emp["EmployeeName"].ToString();
                    row["DepartmentName"] = emp["DepartmentName"].ToString();

                    if (attendanceLookup.TryGetValue(key, out DataRow att))
                    {
                        row["IsAbsent"] = false;
                        row["InTime"] = att["StartTime"] == DBNull.Value ? "" : att["StartTime"].ToString();
                        row["OutTime"] = att["EndTime"] == DBNull.Value ? "" : att["EndTime"].ToString();
                        row["Remarks"] = att["AttendanceType"].ToString();
                    }
                    else
                    {
                        row["IsAbsent"] = true;
                        row["InTime"] = "";
                        row["OutTime"] = "";
                        row["Remarks"] = "";
                    }

                    dt.Rows.Add(row);
                }
            }

            gvDatewiseAttendance.DataSource = dt;
            gvDatewiseAttendance.DataBind();
        }

        protected void gvDatewiseAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;

            DataRowView rowView = (DataRowView)e.Row.DataItem;
            DateTime rowDate = (DateTime)rowView["AttendanceDateEnglish"];
            string nepaliDate = rowView["AttendanceDateNepali"].ToString();
            bool isAbsent = (bool)rowView["IsAbsent"];

            if (rowDate.Date != lastGroupDate.Date)
            {
                lastGroupDate = rowDate;

                string[] bsParts = nepaliDate.Split('-');
                string groupLabel = "Date : " + int.Parse(bsParts[0]) + "/" + int.Parse(bsParts[1]) + "/" + int.Parse(bsParts[2]);

                GridViewRow headerRow = new GridViewRow(0, 0, DataControlRowType.Separator, DataControlRowState.Normal);
                TableCell cell = new TableCell();
                cell.ColumnSpan = e.Row.Cells.Count;
                cell.CssClass = "bg-info text-center";
                cell.Style["color"] = "white";
                cell.Style["font-weight"] = "bold";
                cell.Text = groupLabel;
                headerRow.Cells.Add(cell);

                Table table = (Table)((GridView)sender).Controls[0];
                table.Controls.AddAt(e.Row.RowIndex + insertedGroupRows + 1, headerRow);
                insertedGroupRows++;
            }

            if (isAbsent)
            {
                e.Row.Cells[3].Text = "Absent";
                e.Row.Cells[3].ColumnSpan = 3;
                e.Row.Cells[3].CssClass = "text-center text-muted";
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("datewiseAttendance.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=DatewiseAttendance.xls");
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gvDatewiseAttendance.RenderControl(htw);

            Response.Write(sw.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}