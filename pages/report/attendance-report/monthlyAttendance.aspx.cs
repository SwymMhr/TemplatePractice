using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TemplatingPractice.BLL;
using TemplatingPractice.Utils;

namespace TemplatingPractice.pages.report.attendance_report
{
    public partial class monthlyAttendance : System.Web.UI.Page
    {
        BLLBranch blBranch = new BLLBranch();
        BLLDepartment blDepartment = new BLLDepartment();
        BLLEmployee ble = new BLLEmployee();

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

        private void BindDepartmentsForBranch()
        {
            ddlDepartment.DataSource = blDepartment.GetAllDepartment();
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));
            ddlDepartment.Enabled = true;
        }

        private void BindEmployeesForBranchDepartment(int branchId, int departmentId)
        {
            ddlEmployee.DataSource = ble.GetEmployeeByBranchAndDepartment(branchId, departmentId);
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, new ListItem("Select Employee", ""));
            ddlEmployee.Enabled = true;
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDepartment.Items.Clear();
            ddlEmployee.Items.Clear();
            ddlEmployee.Enabled = false;
            txtEmpId.Text = "";

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ddlDepartment.Enabled = false;
                return;
            }

            BindDepartmentsForBranch();
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlEmployee.Items.Clear();
            txtEmpId.Text = "";

            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue) || string.IsNullOrWhiteSpace(ddlDepartment.SelectedValue))
            {
                ddlEmployee.Enabled = false;
                return;
            }

            int branchId = int.Parse(ddlBranch.SelectedValue);
            int departmentId = int.Parse(ddlDepartment.SelectedValue);

            BindEmployeesForBranchDepartment(branchId, departmentId);
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlEmployee.SelectedValue))
            {
                txtEmpId.Text = "";
                return;
            }

            int employeeId = int.Parse(ddlEmployee.SelectedValue);
            txtEmpId.Text = employeeId.ToString();
        }

        protected void txtEmpId_TextChanged(object sender, EventArgs e)
        {
            int employeeId;
            if (!int.TryParse(txtEmpId.Text.Trim(), out employeeId))
            {
                txtEmpId.Text = "";
                return;
            }

            DataTable empTable = ble.GetEmployeeById(employeeId);
            if (empTable.Rows.Count == 0)
            {
                txtEmpId.Text = "";
                ShowAlert("Employee ID not found.", "error");
                return;
            }

            DataRow empRow = empTable.Rows[0];
            int branchId = Convert.ToInt32(empRow["BranchID"]);
            int departmentId = Convert.ToInt32(empRow["DepartmentID"]);

            BindBranches();
            if (ddlBranch.Items.FindByValue(branchId.ToString()) == null)
            {
                ShowAlert("Employee's branch could not be matched.", "error");
                return;
            }
            ddlBranch.SelectedValue = branchId.ToString();

            BindDepartmentsForBranch();
            if (ddlDepartment.Items.FindByValue(departmentId.ToString()) == null)
            {
                ShowAlert("Employee's department could not be matched.", "error");
                return;
            }
            ddlDepartment.SelectedValue = departmentId.ToString();

            BindEmployeesForBranchDepartment(branchId, departmentId);
            if (ddlEmployee.Items.FindByValue(employeeId.ToString()) == null)
            {
                ShowAlert("Employee could not be matched in the list.", "error");
                return;
            }
            ddlEmployee.SelectedValue = employeeId.ToString();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ddlBranch.SelectedValue))
            {
                ShowAlert("Please select a Branch.", "error");
                return;
            }

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

            string url = "monthlyAttendanceList.aspx"
                + "?BranchID=" + ddlBranch.SelectedValue
                + "&DepartmentID=" + ddlDepartment.SelectedValue
                + "&StartDate=" + startDate.ToString("yyyy-MM-dd")
                + "&EndDate=" + endDate.ToString("yyyy-MM-dd");

            Response.Redirect(url);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("monthlyAttendance.aspx");
        }

        private void ShowAlert(string message, string type)
        {
            string script = $@"swal('{message}', '{type}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
        }
    }
}